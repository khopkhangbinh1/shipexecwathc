package com.ict.ppsedi.view.fragment.edi.pick;

import android.Manifest;
import android.app.AlertDialog;
import android.app.DatePickerDialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;
import android.os.Bundle;
import android.telephony.TelephonyManager;
import android.view.KeyEvent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewTreeObserver;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.LinearLayout;
import android.widget.ScrollView;

import androidx.appcompat.app.AppCompatActivity;
import androidx.core.app.ActivityCompat;
import androidx.core.content.ContextCompat;
import androidx.recyclerview.widget.RecyclerView;

import com.ict.ppsedi.R;
import com.ict.ppsedi.entities.PalletInfoEntity;
import com.ict.ppsedi.entities.T_SHIPMENT_PALLET_EXT;
import com.ict.ppsedi.presenter.shipping.pick.PickSeachPresenter;
import com.ict.ppsedi.utilities.UtilsConstants;
import com.ict.ppsedi.view.BaseFragment;
import com.ict.ppsedi.view.activity.shipping.pick.PickDetailActivity;
import com.ict.ppsedi.view.adapter.pick.PickSearchAdapter;
import com.ict.ppsedi.view.customs.MyToast;
import com.ict.ppsedi.view.customs.VoiceManager;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.Locale;
import java.util.concurrent.Callable;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;

import android.provider.Settings.Secure;
import android.widget.Toast;

public class PickSearchFragment extends BaseFragment implements PickSearchView {

    @BindView(R.id.lvList)
    public RecyclerView lvList;

    @BindView(R.id.etShipment)
    public EditText etShipment;

    @BindView(R.id.etPallet)
    public EditText etPallet;

    @BindView(R.id.etDate)
    public EditText etDate;

    @BindView(R.id.scrLstPick)
    public ScrollView scrLstPick;

    @BindView(R.id.lnHeader)
    public LinearLayout lnHeader;

    @BindView(R.id.btShowControl)
    public ImageButton btShowControl;

    protected ProgressDialog progress;

    final Calendar myCalendar = Calendar.getInstance();

    PickSeachPresenter presenter;

    private String machineCode;
    VoiceManager voiceManager;

    @Override
    public View baseFragmentView(LayoutInflater inflater, ViewGroup parent, Bundle savedInstanceState) {
        View rootView = inflater.inflate(R.layout.fragment_pick_search, parent, false);
        ButterKnife.bind(this, rootView);
        ((AppCompatActivity) getActivity()).getSupportActionBar().setTitle("PICK");
        voiceManager = new VoiceManager(this.getContext());

        this.progress = new ProgressDialog(this.getActivity());
        progress.setTitle("Loading");
        progress.setMessage("Wait while loading...");
        progress.setCancelable(false);

        allListener();
        try {
            String date = new SimpleDateFormat("yyyy-MM-dd", Locale.getDefault()).format(new Date());
            etDate.setText(date);
        } catch (Exception ex) {
        }
        presenter = new PickSeachPresenter();
        presenter.setView(this);
        return rootView;
    }

    @Override
    public void onResume() {
        super.onResume();
        presenter.bindList();
    }

    @OnClick(R.id.btnSearch)
    public void onBtnSearch_Click() {
        Search();
    }

    @OnClick(R.id.btShowControl)
    public void onBtShowControl_Click() {
        int viewStatus = lnHeader.getVisibility();
        if (viewStatus != View.GONE) {
            lnHeader.setVisibility(View.GONE);
            btShowControl.setBackground(ContextCompat.getDrawable(this.getActivity(), R.drawable.ic_baseline_arrow_drop_down_24));
            etShipment.requestFocus();
        } else {
            lnHeader.setVisibility(View.VISIBLE);
            btShowControl.setBackground(ContextCompat.getDrawable(this.getActivity(), R.drawable.ic_baseline_arrow_drop_up_24));
        }
    }

    @OnClick(R.id.btnBindDetail)
    public void onBtnBindDetail_Click() {
        navigationDetail();
    }

    private void navigationDetail() {
        String palletNo = etPallet.getText().toString();
        if (palletNo != null && !palletNo.equals("")) {
            PalletInfoEntity obj = this.presenter.getPalletDetail(palletNo);
            if (obj == null)
                MyToast.show(this.getContext(), "栈板不存在", UtilsConstants.Status.WARNING.name());
            else
                this.presenter.navigationDetail(obj);
        } else
            MyToast.show(this.getContext(), "栈板号不能是空子", UtilsConstants.Status.WARNING.name());
    }

    public void Search() {
        presenter.bindList();
    }

    @Override
    public void bindDetail(String jsonObj) {
        try {
            Intent callingIntent = new Intent(this.getContext(), PickDetailActivity.class);
            callingIntent.putExtra(PickDetailActivity.INTENT_EXTRA_PARAM_PICK_DETAIL, jsonObj);
            this.getActivity().startActivity(callingIntent);

        } catch (Exception ex) {
        }
    }

    @Override
    public String getMachineCode() {
        if (machineCode == null || machineCode.trim().isEmpty())
            machineCode = Secure.getString(getContext().getContentResolver(), Secure.ANDROID_ID);
        return machineCode;
    }

    @Override
    public void showMachineDialog(String msg, String json, String palletNo) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this.getContext());
        builder.setMessage(msg)
                .setCancelable(false)
                .setPositiveButton("YES", (dialog, id) -> {
                    try {
                        bindDetail(json);
                    } catch (Exception ex) {
                    }
                })
                .setNegativeButton("NO", (dialog, which) -> {
                    dialog.dismiss();
                    presenter.finishPick(palletNo);
                });

        AlertDialog alert = builder.create();
        alert.show();
    }

//    @Override
//    public void showMachineDialog(String msg, Callable call) {
//        AlertDialog.Builder builder = new AlertDialog.Builder(this.getContext());
//        builder.setMessage(msg)
//                .setCancelable(false)
//                .setPositiveButton("YES", (dialog, id) -> {
//                    try {
//                        call.call();
//                    } catch (Exception ex) {
//                    }
//                })
//                .setNegativeButton("NO", (dialog, which) -> {
//                    dialog.dismiss();
//                });
//
//        AlertDialog alert = builder.create();
//        alert.show();
//    }

    public void allListener() {
        DatePickerDialog.OnDateSetListener date = (view, year, monthOfYear, dayOfMonth) -> {
            // TODO Auto-generated method stub
            myCalendar.set(Calendar.YEAR, year);
            myCalendar.set(Calendar.MONTH, monthOfYear);
            myCalendar.set(Calendar.DAY_OF_MONTH, dayOfMonth);
            formatDate();
        };

        etDate.setOnClickListener(v -> {
            // TODO Auto-generated method stub
            new DatePickerDialog(getContext(), date, myCalendar
                    .get(Calendar.YEAR), myCalendar.get(Calendar.MONTH),
                    myCalendar.get(Calendar.DAY_OF_MONTH)).show();
        });

        etShipment.setOnKeyListener((v, keyCode, event) -> {
            if (keyCode == UtilsConstants.KEY_SCAN_PDA && event.getAction() == KeyEvent.ACTION_UP
                    && !etShipment.getText().toString().equals("")) {
                voiceManager.playOK();
                Search();
                etShipment.setText("");
                etShipment.requestFocus();
                return true;
            }
            return false;
        });

        etPallet.setOnKeyListener((v, keyCode, event) -> {
            if (keyCode == UtilsConstants.KEY_SCAN_PDA && event.getAction() == KeyEvent.ACTION_UP
                    && !etPallet.getText().toString().equals("")) {
                etPallet.setText("");
                navigationDetail();
                return true;
            }
            return false;
        });


        etShipment.requestFocus();
    }

    private void formatDate() {
        String myFormat = "yyyy-MM-dd";
        SimpleDateFormat sdf = new SimpleDateFormat(myFormat, Locale.US);
        etDate.setText(sdf.format(myCalendar.getTime()));
    }

    @Override
    public void bindList(ArrayList<PalletInfoEntity> lstPallet) {
        if (lstPallet == null || lstPallet.size() <= 0) {
            lvList.setAdapter(null);
        } else {
            PickSearchAdapter adapter = new PickSearchAdapter(getContext(), lstPallet, presenter);
            lvList.setAdapter(adapter);
        }
    }

    @Override
    public String getShipDate() {
        return etDate.getText().toString();
    }

    @Override
    public String getShipmentID() {
        return etShipment.getText().toString();
    }

    @Override
    public void showMsg(String msg, String type) {
        this.getActivity().runOnUiThread(() -> MyToast.show(getContext(), msg, type));
    }

    @Override
    public void showDialog() {
        progress.show();
    }

    @Override
    public void closeDialog() {
        progress.dismiss();
    }
}
