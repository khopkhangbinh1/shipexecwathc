package com.ict.ppsedi.view.fragment.edi.pick;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.provider.Settings;
import android.view.KeyEvent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.RecyclerView;

import com.ict.ppsedi.R;
import com.ict.ppsedi.model.PickModel;
import com.ict.ppsedi.utilities.UtilsConstants;
import com.ict.ppsedi.view.BaseFragment;
import com.ict.ppsedi.view.activity.shipping.pick.PickPrintLabelActivity;
import com.ict.ppsedi.view.customs.MyToast;

import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.Locale;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;

public class PickReprintFragment extends BaseFragment {

    @BindView(R.id.etCarton)
    public EditText etCarton;


    protected ProgressDialog progress;
    final Calendar myCalendar = Calendar.getInstance();

    PickModel model;
    String machineCode;

    @Override
    public View baseFragmentView(LayoutInflater inflater, ViewGroup parent, Bundle savedInstanceState) {
        View rootView = inflater.inflate(R.layout.pick_reprint_fragment, parent, false);
        ButterKnife.bind(this, rootView);
        ((AppCompatActivity) getActivity()).getSupportActionBar().setTitle("PICK Reprint");
        setEvenListener();
        return rootView;
    }

    private void setEvenListener() {
        etCarton.requestFocus();
        etCarton.setOnKeyListener((v, keyCode, event) -> {
            if (keyCode == UtilsConstants.KEY_SCAN_PDA && event.getAction() == KeyEvent.ACTION_UP) {
                if (!etCarton.getText().toString().equals("")) {
                    search(etCarton.getText().toString());
                }
                return true;
            }
            return false;
        });
    }

    @OnClick(R.id.btnSearch)
    public void btnSearch_OnClick() {
        String cartonNo = etCarton.getText().toString().trim();
        if (cartonNo != null && cartonNo != "")
            search(cartonNo);
        else
            MyToast.show(this.getContext(), "Carton cannot empty", UtilsConstants.Status.WARNING.name());
    }

    public void search(String cartonNo) {
        Intent goToNextActivity = new Intent(this.getContext(), PickPrintLabelActivity.class);
        goToNextActivity.putExtra(PickPrintLabelActivity.INTENT_EXTRA_PARAM_PICK_BARCODE, cartonNo);
        startActivity(goToNextActivity);
    }
}
