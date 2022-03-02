package com.ict.ppsedi.view.activity.shipping.pick;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.provider.Settings;
import android.view.KeyEvent;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import androidx.appcompat.app.ActionBarDrawerToggle;
import androidx.appcompat.widget.Toolbar;
import androidx.core.content.ContextCompat;
import androidx.drawerlayout.widget.DrawerLayout;
import androidx.recyclerview.widget.RecyclerView;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.ict.ppsedi.R;
import com.ict.ppsedi.entities.PalletInfoEntity;
import com.ict.ppsedi.entities.PalletPartEntity;
import com.ict.ppsedi.entities.PickPartLocationEntity;
import com.ict.ppsedi.entities.PickSNEntity;
import com.ict.ppsedi.presenter.initialization.login.LoginPresenter;
import com.ict.ppsedi.presenter.shipping.pick.PickDetailPresenter;
import com.ict.ppsedi.presenter.shipping.pick.PickSeachPresenter;
import com.ict.ppsedi.utilities.UserUtils;
import com.ict.ppsedi.utilities.UtilsConstants;
import com.ict.ppsedi.view.BaseActivity;
import com.ict.ppsedi.view.activity.initialization.login.LoginView;
import com.ict.ppsedi.view.adapter.pick.PickPalletPartAdapter;
import com.ict.ppsedi.view.adapter.pick.PickPartLocationAdapter;
import com.ict.ppsedi.view.adapter.pick.PickSearchAdapter;
import com.ict.ppsedi.view.customs.MyToast;
import com.ict.ppsedi.view.customs.VoiceManager;

import java.util.ArrayList;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;
import butterknife.OnLongClick;

public class PickDetailActivity extends BaseActivity implements PickDetailView {

    @BindView(R.id.etCarton)
    public EditText etCarton;

    @BindView(R.id.tvPalletNo)
    public TextView tvPalletNo;

    @BindView(R.id.tvShipment)
    public TextView tvShipment;

    @BindView(R.id.tvCarrier)
    public TextView tvCarrier;

    @BindView(R.id.tvPriority)
    public TextView tvPriority;

    @BindView(R.id.tvPackcodedesc)
    public TextView tvPackcodedesc;

    @BindView(R.id.tvQty)
    public TextView tvQty;

    @BindView(R.id.tvPickPallet)
    public TextView tvPickPallet;

    @BindView(R.id.lvPalletPart)
    public RecyclerView lvPalletPart;

    @BindView(R.id.lvPartLocation)
    public RecyclerView lvPartLocation;

    @BindView(R.id.lnPalletPick)
    public LinearLayout lnPalletPick;

    @BindView(R.id.lnPartLocation)
    public LinearLayout lnPartLocation;

    @BindView(R.id.btShowPalletPart)
    public ImageButton btShowPalletPart;

    @BindView(R.id.btShowPartLocation)
    public ImageButton btShowPartLocation;

    public static final String INTENT_EXTRA_PARAM_PICK_DETAIL = "view.activity.shipping.pick.PickDetailActivity";

    PickDetailPresenter presenter;
    private VoiceManager voiceManager;

    PickSNEntity objSN;
    private String machineCode;
    private String lastCarton;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.pick_detail_activity);
        ButterKnife.bind(this);

        assert getSupportActionBar() != null;   //null check
        getSupportActionBar().setDisplayHomeAsUpEnabled(false);   //dont show back button
        getSupportActionBar().setTitle("Pick Detail");

        voiceManager = new VoiceManager(this);
        presenter = new PickDetailPresenter();
        presenter.setView(this);

        startData();
        setEvenListener();
    }

    public void startData() {
        String json = (String) getIntent().getSerializableExtra(INTENT_EXTRA_PARAM_PICK_DETAIL);
        Gson gson = new Gson();
        PalletInfoEntity obj = gson.fromJson(json, new TypeToken<PalletInfoEntity>() {
        }.getType());
        tvPalletNo.setText(obj.getPalletNo());
        tvShipment.setText(obj.getShipmentID());
        tvCarrier.setText(obj.getCarrierName());

        if (machineCode == null || machineCode.trim().isEmpty())
            machineCode = Settings.Secure.getString(this.getContentResolver(), Settings.Secure.ANDROID_ID);

        objSN = new PickSNEntity();
        objSN.setShipmentId(obj.getShipmentID());
        objSN.setPalletid(obj.getPalletNo());
        objSN.setEmpNo(UserUtils.getUser().getUserID());
        objSN.setUUID(machineCode);

    }

    @Override
    public void onResume() {
        super.onResume();
        etCarton.requestFocus();
//        presenter.refreshPick();
        presenter.bindPalletPart();
        presenter.bindPalletDetail(false);

//        presenter.bindPartLocation("ALL");
    }

    // create an action bar button
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.activity_pick_menu, menu);
        return super.onCreateOptionsMenu(menu);
    }


    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case android.R.id.home:
                backPress();
                return true;
            case R.id.btnRefresh:
                presenter.refreshPick();
                return true;
        }
        return super.onOptionsItemSelected(item);
    }

    public void backPress() {
//        this.presenter.finishPick();
        backActivity();
    }

    @Override
    public void onBackPressed() {
        //cannot back by press button
    }

    @Override
    public void showMsg(String msg, String type) {
        this.runOnUiThread(() -> MyToast.show(this, msg, type));
    }

    @Override
    public void showDialog() {
        progress.show();
    }

    @Override
    public void closeDialog() {
        progress.dismiss();
    }

    @Override
    public String getPalletNo() {
        return tvPalletNo.getText().toString();
    }

    @Override
    public void bindDetail(PalletInfoEntity objDetail) {
        tvPackcodedesc.setText(objDetail.getPackcodedesc());
        tvPriority.setText(objDetail.getPriority());
        tvQty.setText(objDetail.getDealQty() + "/" + objDetail.getQty());
    }

    @Override
    public void playOK() {
        voiceManager.playOK();
    }

    @Override
    public void playNG() {
        voiceManager.playNG();
    }

    @Override
    public PickSNEntity getSN() {
        objSN.setCTNNO(etCarton.getText().toString());
        return objSN;
    }

    @Override
    public String getLastPickCarton() {
        return lastCarton;
    }

    @Override
    public void setLastPickCarton(String carton) {
        lastCarton = carton;
    }

    @Override
    public void setSN(PickSNEntity obj) {
        objSN.setPickPalletNo(obj.getPickPalletNo());
//        objSN = obj;
    }

    @Override
    public void scanNewCarton() {
        etCarton.setText("");
        etCarton.requestFocus();

    }

    @Override
    public void backActivity() {
        Intent moveResult = new Intent();
        this.setResult(Activity.RESULT_OK, moveResult);
        this.finish();
    }

    @Override
    public void generateLabel(String objSN) {
        Intent goToNextActivity = new Intent(getApplicationContext(), PickPrintLabelActivity.class);
        goToNextActivity.putExtra(PickPrintLabelActivity.INTENT_EXTRA_PARAM_PICK_BARCODE, objSN);
        startActivity(goToNextActivity);
    }

    @OnClick(R.id.btnPick)
    public void btnPick_OnClick() {
        if (!etCarton.getText().toString().equals("")) {
            this.presenter.pick(etCarton.getText().toString());
        }
    }

    @OnLongClick(R.id.btnFinish)
    public boolean btnFinish_OnClick() {
        this.presenter.finishPick();
        return true;
    }

    @OnClick(R.id.btnPrint)
    public void btnPrint_OnClick() {
        Intent goToNextActivity = new Intent(getApplicationContext(), PickPrintLabelActivity.class);
        goToNextActivity.putExtra(PickPrintLabelActivity.INTENT_EXTRA_PARAM_PICK_BARCODE, "1LA2009050925567980001");
        startActivity(goToNextActivity);
    }

    @OnClick(R.id.btShowPalletPart)
    public void onBtShowPalletPart_Click() {
        int viewStatus = lnPalletPick.getVisibility();
        if (viewStatus != View.GONE) {
            lnPalletPick.setVisibility(View.GONE);
            btShowPalletPart.setBackground(ContextCompat.getDrawable(this, R.drawable.ic_baseline_arrow_drop_down_24));
            etCarton.requestFocus();
        } else {
            lnPalletPick.setVisibility(View.VISIBLE);
            btShowPalletPart.setBackground(ContextCompat.getDrawable(this, R.drawable.ic_baseline_arrow_drop_up_24));
        }
    }

    @OnClick(R.id.btShowPartLocation)
    public void onBtShowPartLocation_Click() {
        int viewStatus = lnPartLocation.getVisibility();
        if (viewStatus != View.GONE) {
            lnPartLocation.setVisibility(View.GONE);
            btShowPartLocation.setBackground(ContextCompat.getDrawable(this, R.drawable.ic_baseline_arrow_drop_down_24));
            etCarton.requestFocus();
        } else {
            lnPartLocation.setVisibility(View.VISIBLE);
            btShowPartLocation.setBackground(ContextCompat.getDrawable(this, R.drawable.ic_baseline_arrow_drop_up_24));
        }
    }

    @Override
    public void bindPalletPart(ArrayList<PalletPartEntity> lstPart) {
        if (lstPart == null || lstPart.size() <= 0) {
            lvPalletPart.setAdapter(null);
        } else {
            PickPalletPartAdapter adapter = new PickPalletPartAdapter(this, lstPart, presenter);
            lvPalletPart.setAdapter(adapter);
        }
    }

    @Override
    public void bindPartLocation(ArrayList<PickPartLocationEntity> lst) {
        if (lst == null || lst.size() <= 0) {
            lvPartLocation.setAdapter(null);
        } else {
            PickPartLocationAdapter adapter = new PickPartLocationAdapter(this, lst);
            lvPartLocation.setAdapter(adapter);
        }
    }

    @Override
    public void bindPickPalletNo() {
        tvPickPallet.setText(objSN.getPickPalletNo());
    }


    private void setEvenListener() {
        etCarton.setOnKeyListener((v, keyCode, event) -> {
            if (keyCode == UtilsConstants.KEY_SCAN_PDA && event.getAction() == KeyEvent.ACTION_UP) {
                if (!etCarton.getText().toString().equals("")) {
                    String carton = etCarton.getText().toString();
                    this.presenter.pick(carton);

                }
            }
            return false;
        });
    }

    @Override
    public void showToastTest(String msg) {
        Toast.makeText(getBaseContext(), msg, Toast.LENGTH_SHORT).show();
    }
}
