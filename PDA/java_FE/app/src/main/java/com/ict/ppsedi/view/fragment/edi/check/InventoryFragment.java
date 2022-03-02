package com.ict.ppsedi.view.fragment.edi.check;


import android.app.DatePickerDialog;
import android.content.Context;
import android.database.sqlite.SQLiteCursor;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.KeyEvent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.LinearLayout;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.Spinner;

import androidx.core.content.ContextCompat;
import androidx.recyclerview.widget.RecyclerView;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.ict.ppsedi.entities.InventoryEntity;

import com.ict.ppsedi.R;
import com.ict.ppsedi.entities.ExecuteResult;
import com.ict.ppsedi.entities.InventorySNEntity;
import com.ict.ppsedi.entities.PalletInfoEntity;
import com.ict.ppsedi.entities.PickLogEntity;
import com.ict.ppsedi.entities.PickSNEntity;
import com.ict.ppsedi.model.InventoryModel;
import com.ict.ppsedi.utilities.UserUtils;
import com.ict.ppsedi.utilities.UtilsConstants;
import com.ict.ppsedi.view.BaseFragment;
import com.ict.ppsedi.view.activity.shipping.pick.PickDetailView;
import com.ict.ppsedi.view.adapter.check.InventoryAdapter;
import com.ict.ppsedi.view.adapter.check.InventoryAdapterCheckLog;
import com.ict.ppsedi.view.adapter.check.InventoryAdapterList;
import com.ict.ppsedi.view.adapter.check.InventoryAdapterLocationSNInfo;
import com.ict.ppsedi.view.adapter.check.InventoryAdapterSNResult;
import com.ict.ppsedi.view.customs.MyToast;
import com.ict.ppsedi.view.customs.VoiceManager;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Calendar;
import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;

import static android.view.View.GONE;
import static android.view.View.INVISIBLE;
import static android.view.View.VISIBLE;

public class InventoryFragment extends BaseFragment {

    @BindView(R.id.cmbWHID)
    public Spinner cmbWHID;
    @BindView(R.id.lnHeader)
    public LinearLayout lnHeader;

    @BindView(R.id.checkLayout3)
    public LinearLayout checkLayout3;
    @BindView(R.id.checkLayout2)
    public LinearLayout checkLayout2;
    @BindView(R.id.checkLayout1)
    public LinearLayout checkLayout1;

    @BindView(R.id.etCarton)
    public EditText etCarton;
    @BindView(R.id.txtQTY)
    public EditText txtQTY;

    @BindView(R.id.txtSN)
    public EditText txtSN;
    @BindView(R.id.txtSN2)
    public EditText txtSN2;

    @BindView(R.id.RadioGroup23)
    public RadioGroup RadioGroup23;

    @BindView(R.id.chkQHold)
    public CheckBox chkQHold;

    InventorySNEntity objSN;

    @BindView(R.id.rdoSN)
    public RadioButton rdoSN;

    @BindView(R.id.rdoQTY)
    public RadioButton rdoQTY;
    String WHID;
    @BindView(R.id.lvList)
    public RecyclerView lvList;
    @BindView(R.id.lvList1)
    public RecyclerView lvList1;
    @BindView(R.id.lvList2)
    public RecyclerView lvList2;
    //InventoryAdapter objSN;
    @BindView(R.id.btnPallet)
    public Button btnPallet;

    @BindView(R.id.btListControl)
    public ImageButton btListControl;

    @BindView(R.id.btList1Control)
    public ImageButton btList1Control;

    @BindView(R.id.btList2Control)
    public ImageButton btList2Control;

    public String locationID;
    public String locationNo;
    public String palletNo ="";
    int checksearch = 0;
    public String txtLocationNo;

    private VoiceManager voiceManager;

    InventoryModel model = new InventoryModel();
    final ExecuteResult[] res = {new ExecuteResult()};
    String whsID = null;
    String PalletID = null;
    String IsFirst = "Y";

    ArrayList<InventoryEntity> lstSNResult = new ArrayList();
    InventoryAdapterLocationSNInfo adapterRoot;
    ArrayList<InventoryEntity> lstSNInfo = new ArrayList();
    ArrayList<InventoryEntity> lstSNLog = new ArrayList();
    ArrayList<InventoryEntity> lstDetail = new ArrayList();
    ArrayList<InventoryEntity> lstResult = new ArrayList();

    @Override
    public View baseFragmentView(LayoutInflater inflater, ViewGroup parent, Bundle savedInstanceState) {
        View rootView = inflater.inflate(R.layout.activity_inventory, parent, false);
        ButterKnife.bind(this, rootView);
        voiceManager = new VoiceManager(getContext());
        bindwhs();
        rdoSN.setChecked(true);
        checkLayout2.setVisibility(INVISIBLE);
        checkLayout3.setVisibility(INVISIBLE);
        checkLayout1.setVisibility(VISIBLE);
        btnPallet.setVisibility(INVISIBLE);

        RadioGroup23.setOnCheckedChangeListener(new RadioGroup.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(RadioGroup arg0, int selectedId) {
                selectedId = RadioGroup23.getCheckedRadioButtonId();
                int a = selectedId;
                checkData();
//                if (rdoQTY.isChecked()) {
//                    //etCarton.setText("VNFG-Q37-A02");
//                    checkLayout2.setVisibility(VISIBLE);
//                    checkLayout3.setVisibility(VISIBLE);
//                    checkLayout1.setVisibility(VISIBLE);
//                    btnPallet.setVisibility(VISIBLE);
//                    btnPallet.setEnabled(true);
//                    txtSN.setText(palletNo);
//                    txtSN2.setText("");
//                    txtSN2.requestFocus();
//                } else {
//                    //etCarton.setText("VNFG-Q37-A02");
//                    txtSN.setText("");
//                    txtSN.requestFocus();
//                    checkLayout2.setVisibility(INVISIBLE);
//                    checkLayout3.setVisibility(INVISIBLE);
//                    checkLayout1.setVisibility(VISIBLE);
//                    btnPallet.setEnabled(false);
//                    btnPallet.setVisibility(INVISIBLE);
//                }
            }
        });
        setEvenListener();
        return rootView;
    }

    public void setSN(InventorySNEntity obj) {
        objSN = obj;
    }

    public void bindwhs() {

        final ExecuteResult[] res = {new ExecuteResult()};
        new AsyncTask<Void, Void, String>() {
            @Override
            protected String doInBackground(Void... voids) {
                res[0] = model.getWHS();
                return "OK";
            }
            @Override
            protected void onPreExecute() {
                super.onPreExecute();
            }

            protected void onPostExecute(String result) {

                super.onPostExecute(result);
                if (res[0].isSuccess()) {
                    ArrayList<InventoryEntity> lst = new Gson().fromJson(res[0].getData().toString(), new TypeToken<ArrayList<InventoryEntity>>() {
                    }.getType());
                    InventoryAdapter adapter = new InventoryAdapter(InventoryFragment.this,
                            R.layout.spinner_item_inventory,
                            R.id.cmbWHNAME,
                            //   R.id.textView_item_percent,
                            lst);
                    cmbWHID.setAdapter(adapter);
                    cmbWHID.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
                        @Override
                        public void onItemSelected(AdapterView<?> parent,
                                                   View view, int pos, long id) {
                            whsID = lst.get((pos)).getId();
                            etCarton.requestFocus();
                        }

                        @Override
                        public void onNothingSelected(AdapterView<?> arg0) {
                            // TODO Auto-generated method stub
                        }
                    });

                    MyToast.show(getContext(), "OK", UtilsConstants.Status.SUCCESS.name());
                } else {
                    MyToast.show(getContext(), res[0].getMessage(), UtilsConstants.Status.ERROR.name());
                }
            }
        }.execute();
    }

    public void bindatalocationDetail(String whID, String LocationNO) {
        Context _context = getContext();
        final ExecuteResult[] res = {new ExecuteResult()};
        new AsyncTask<Void, Void, String>() {
            @Override
            protected String doInBackground(Void... voids) {
                res[0] = model.getdatalocationDetail(whID, LocationNO);
                return "OK";
            }

            @Override
            protected void onPreExecute() {
                super.onPreExecute();
            }

            protected void onPostExecute(String result) {
                super.onPostExecute(result);
                if (res[0].isSuccess()) {
                    lstDetail = new Gson().fromJson(res[0].getData().toString(), new TypeToken<ArrayList<InventoryEntity>>() {
                    }.getType());
                    InventoryAdapterList adapter = new InventoryAdapterList(getContext(), lstDetail);
                    PalletID = lstDetail.get((0)).getPalletNo();
                    lvList.setAdapter(adapter);
                    locationID = lstDetail.get((0)).getLocationID();
                    palletNo = lstDetail.get(0).getPalletNo();
                    GetLocationCheckLog(txtLocationNo);
                    checkData();
                  checksearch = 1;
//                    txtSN.setText("");
//                    txtSN.requestFocus();
                    voiceManager.playOK();
                } else {
                    checksearch = 0;
                    voiceManager.playNG();
                    etCarton.setText("");
                    MyToast.show(_context, res[0].getMessage(), UtilsConstants.Status.ERROR.name());
                }
            }
        }.execute();
    }

    @OnClick(R.id.btnSearch)
    public void onBtnSearch_Click() {
        Search();
    }

    private void Search() {
        palletNo ="";
        lstSNResult = new ArrayList();
        txtLocationNo = etCarton.getText().toString();
        InventoryAdapterLocationSNInfo.setListSNDone(lstSNResult);
        //lvList3.setAdapter(null);
       int a = 1;
        if (!txtLocationNo.equals("")) {
            bindatalocationDetail(whsID, txtLocationNo);
          //  int bbb =checksearch;

//            if (checksearch == 1){
//                checkData();
//            }
        }
        IsFirst = "Y";
    }



    public void GetLocationCheckLog(String LocationNO) {
        Context _context = getContext();
        final ExecuteResult[] res = {new ExecuteResult()};
        new AsyncTask<Void, Void, String>() {
            @Override
            protected String doInBackground(Void... voids) {
                res[0] = model.GetLocationCheckLog(LocationNO);
                return "OK";
            }

            @Override
            protected void onPreExecute() {
                super.onPreExecute();
            }

            protected void onPostExecute(String result) {
                super.onPostExecute(result);
                if (res[0].isSuccess()) {
//                    lstSNLog = new Gson().fromJson(res[0].getData().toString(), new TypeToken<ArrayList<InventoryEntity>>() {
//                    }.getType());
                    ArrayList lstTemp= new Gson().fromJson(res[0].getData().toString(), new TypeToken<ArrayList<InventoryEntity>>() {
                                           }.getType());
                    InventoryAdapterCheckLog adapter = new InventoryAdapterCheckLog(getContext(), lstTemp);
                    lvList2.setAdapter(null);
                    lvList2.setAdapter(adapter);
                    getLocationSnInfo(txtLocationNo);
                } else {

                    getLocationSnInfo(txtLocationNo);
                }
            }
        }.execute();
    }

    private void setEvenListener() {
        etCarton.setOnKeyListener((v, keyCode, event) -> {
            if (keyCode == UtilsConstants.KEY_SCAN_PDA && event.getAction() == KeyEvent.ACTION_UP) {
                if (!etCarton.getText().toString().equals("")) {
                    locationNo = etCarton.getText().toString();
                    Search();
                   // int a = 1;

//                    if (checksearch == 1 && a == 1) {
//
//                        txtSN.setText("");
//                        txtSN.requestFocus();
//                    }

                }
            }
            return false;
        });
        txtSN.setOnKeyListener((v, keyCode, event) -> {
            if (keyCode == UtilsConstants.KEY_SCAN_PDA && event.getAction() == KeyEvent.ACTION_UP) {
                if (!txtSN.getText().toString().equals("")) {

                    InventoryCTN();
                }
            }
            return false;
        });
        txtSN2.setOnKeyListener((v, keyCode, event) -> {
            if (keyCode == UtilsConstants.KEY_SCAN_PDA && event.getAction() == KeyEvent.ACTION_UP) {
                if (!txtSN2.getText().toString().equals("")) {

                    InventoryCTN2();

                }
            }
            return false;
        });
    }

    private String lastCarton;

    private void InventoryPallet() {

        Context _context = getContext();
        String checkCTN;
        String checkCTN2;
        final ExecuteResult[] res = {new ExecuteResult()};
        String checkHold;
        if (chkQHold.isChecked()) {
            checkHold = "Y";
        } else checkHold = "N";
        if (rdoQTY.isChecked()) {
            checkCTN = "N";
            checkCTN2 = "Y";
        } else {
            checkCTN2 = "N";
            checkCTN = "Y";
        }
        InventorySNEntity objSN = new InventorySNEntity();
        objSN.setCTNNO(txtSN.getText().toString());
        objSN.setCTN2(txtSN2.getText().toString());
        objSN.setLocationID(locationID);
        objSN.setPalletCartonQTY(txtQTY.getText().toString());
        objSN.setIsFirst(IsFirst);
        objSN.setCheckHold(checkHold);
        objSN.setPalletCartonQTY(txtQTY.getText().toString());
        objSN.setEmpNo(UserUtils.getUser().getUserID());
        objSN.setCheckCTN(checkCTN);
        objSN.setCheckCNTPllet(checkCTN2);
        new AsyncTask<Void, Void, String>() {
            @Override
            protected String doInBackground(Void... voids) {
                res[0] = model.checkPallet(objSN);
                return "OK";
            }

            @Override
            protected void onPreExecute() {
                super.onPreExecute();
//                view.showDialog();
            }

            protected void onPostExecute(String result) {
                super.onPostExecute(result);
                if (res[0].isSuccess()) {
                    //bindatalocationDetail(whsID, txtLocationNo);
                    GetLocationCheckLog(txtLocationNo);
                    GetSnInfoResult(txtSN.getText().toString());
                    if (res[0].getMessage().equals("OK-FINISH")) {
                        FINISH();
                    }
                    voiceManager.playOK();
                    MyToast.show(_context, res[0].getMessage(), UtilsConstants.Status.SUCCESS.name());

                } else {
                    voiceManager.playNG();
                    MyToast.show(_context, res[0].getMessage(), UtilsConstants.Status.ERROR.name());
                }
            }
        }.execute();
    }

    private void getLocationSnInfo(String locationNo) {
        Context _context = getContext();
        final ExecuteResult[] res = {new ExecuteResult()};
        new AsyncTask<Void, Void, String>() {
            @Override
            protected String doInBackground(Void... voids) {
                res[0] = model.getLocationSnInfo(locationNo);
                return "OK";
            }

            @Override
            protected void onPreExecute() {
                super.onPreExecute();
            }

            protected void onPostExecute(String result) {

                super.onPostExecute(result);
                if (res[0].isSuccess()) {
                    lstSNInfo = new Gson().fromJson(res[0].getData().toString(), new TypeToken<ArrayList<InventoryEntity>>() {
                    }.getType());
                    adapterRoot = new InventoryAdapterLocationSNInfo(getContext(), lstSNInfo);

                    lvList1.setAdapter(adapterRoot);
                    checksearch =1;
                } else {
                    etCarton.setText("");
                    MyToast.show(_context, res[0].getMessage(), UtilsConstants.Status.ERROR.name());
                }
            }
        }.execute();
    }

    private void InventoryCTN() {

        Context _context = getContext();
        String checkCTN;
        String checkCTN2;
        final ExecuteResult[] res = {new ExecuteResult()};
        String checkHold;
        if (chkQHold.isChecked()) {
            checkHold = "Y";
        } else checkHold = "N";
        if (rdoQTY.isChecked()) {
            checkCTN = "N";
            checkCTN2 = "Y";
        } else {
            checkCTN2 = "N";
            checkCTN = "Y";
        }
        if (checkCTN == "Y") {

            InventorySNEntity objSN = new InventorySNEntity();
            objSN.setCTNNO(txtSN.getText().toString());
            objSN.setCTN2(txtSN2.getText().toString());
            objSN.setLocationID(locationID);
            objSN.setIsFirst(IsFirst);
            objSN.setCheckHold(checkHold);
            objSN.setPalletCartonQTY(txtQTY.getText().toString());
            objSN.setEmpNo(UserUtils.getUser().getUserID());
            objSN.setCheckCTN(checkCTN);
            objSN.setCheckCNTPllet(checkCTN2);
            String checksn = "Y";

            if (lstSNResult.size() > 0) {
                for (int i = 0; i < lstSNResult.size(); i++) {

                    if (lstSNResult.get(i).getCTNSN().equals(txtSN.getText().toString())) {
                        checksn = "N";
                        txtSN.setText("");
                        txtSN.requestFocus();
                        voiceManager.playNG();
                        MyToast.show(_context, "NG-输入的箱号重复刷入！", UtilsConstants.Status.ERROR.name());
                    }
                }
            }
            if (checksn == "Y") {
                new AsyncTask<Void, Void, String>() {
                    @Override
                    protected String doInBackground(Void... voids) {
                        res[0] = model.inventoryCTN(objSN);
                        return "OK";
                    }

                    @Override
                    protected void onPreExecute() {
                        super.onPreExecute();
//                view.showDialog();
                    }

                    protected void onPostExecute(String result) {
                        super.onPostExecute(result);
                        if (res[0].isSuccess()) {
                          //  bindatalocationDetail(whsID, txtLocationNo);
                            GetLocationCheckLog(txtLocationNo);
                            GetSnInfoResult(txtSN.getText().toString());
                            IsFirst = "N";
                            if (res[0].getMessage().equals("OK")) {
                                txtSN.setText("");
                                //txtSN.requestFocus();
                            } else if (res[0].getMessage().equals("OK-FINISH")) {
                                FINISH();
                            }
                            voiceManager.playOK();
                            MyToast.show(_context, res[0].getMessage() , UtilsConstants.Status.SUCCESS.name());
                        } else {
                            if (IsFirst.equals("Y"))
                                IsFirst = "Y";
                            else
                                IsFirst = "N";
                            if (!res[0].getMessage().substring(0,3).equals("QH:") && !res[0].getMessage().substring(0,3).equals("NG-"))
                            {
                                GetSnInfoResult(txtSN.getText().toString());
                                GetLocationCheckLog(txtLocationNo);
                            }
                            txtSN.setText("");
                            txtSN.requestFocus();
                            voiceManager.playNG();
                            MyToast.show(_context, res[0].getMessage(), UtilsConstants.Status.ERROR.name());
                        }
                    }
                }.execute();
            }

        } else {
            voiceManager.playOK();
            MyToast.show(_context, "OK", UtilsConstants.Status.SUCCESS.name());
            MyToast.show(_context, res[0].getMessage(), UtilsConstants.Status.SUCCESS.name());
            txtSN2.setText("");
            txtSN2.requestFocus();
            IsFirst = "Y";
            txtSN2.setVisibility(VISIBLE);
            txtQTY.setVisibility(VISIBLE);
        }
    }

    public void GetSnInfoResult(String insn) {
        Context _context = getContext();
        final ExecuteResult[] res = {new ExecuteResult()};
        new AsyncTask<Void, Void, String>() {
            @Override
            protected String doInBackground(Void... voids) {
                res[0] = model.GetSnInfoResult(insn);
                return "OK";
            }
            @Override
            protected void onPreExecute() {
                super.onPreExecute();
            }

            protected void onPostExecute(String result) {
                super.onPostExecute(result);
                if (res[0].isSuccess()) {
                   ArrayList lstTemp = new Gson().fromJson(res[0].getData().toString(), new TypeToken<ArrayList<InventoryEntity>>() {
                    }.getType());
                    lstSNResult.addAll(lstTemp);
                    //InventoryAdapterSNResult adapter = new InventoryAdapterSNResult(getContext(), lstSNResult);
                  //  lvList3.setAdapter(adapter);
                    InventoryAdapterLocationSNInfo.setListSNDone(lstSNResult);
                    adapterRoot.notifyDataSetChanged();
                } else {
                    MyToast.show(_context, res[0].getMessage(), UtilsConstants.Status.ERROR.name());
                }
            }
        }.execute();
    }
    @BindView(R.id.layoutLocationDetail)
    public LinearLayout layoutLocationDetail;
    @OnClick(R.id.lvListControl)
    public void onlvListControl_Click() {

        int viewStatus = layoutLocationDetail.getVisibility();
        if (viewStatus != View.GONE) {
            layoutLocationDetail.setVisibility(View.GONE);
            btList1Control.setBackground(ContextCompat.getDrawable(getContext(), R.drawable.ic_baseline_arrow_drop_down_24));

        } else {
            layoutLocationDetail.setVisibility(View.VISIBLE);
            btList1Control.setBackground(ContextCompat.getDrawable(getContext(), R.drawable.ic_baseline_arrow_drop_up_24));
        }
    }

    @BindView(R.id.layoutSN)
    public LinearLayout layoutSN;
    @OnClick(R.id.lvList1Control)
    public void onlvList1Control_Click() {

        int viewStatus = layoutSN.getVisibility();
        if (viewStatus != View.GONE) {
            layoutSN.setVisibility(View.GONE);
            btList1Control.setBackground(ContextCompat.getDrawable(getContext(), R.drawable.ic_baseline_arrow_drop_down_24));

        } else {
            layoutSN.setVisibility(View.VISIBLE);
            btList1Control.setBackground(ContextCompat.getDrawable(getContext(), R.drawable.ic_baseline_arrow_drop_up_24));
        }
    }
    @BindView(R.id.layoutResult)
    public LinearLayout layoutResult;
    @OnClick(R.id.lvList2Control)
    public void onlvList2Control_Click() {
        int viewStatus = layoutResult.getVisibility();
        if (viewStatus != View.GONE) {
            layoutResult.setVisibility(View.GONE);
            btList2Control.setBackground(ContextCompat.getDrawable(getContext(), R.drawable.ic_baseline_arrow_drop_down_24));

        } else {
            layoutResult.setVisibility(View.VISIBLE);
            btList2Control.setBackground(ContextCompat.getDrawable(getContext(), R.drawable.ic_baseline_arrow_drop_up_24));
        }
    }

    private void InventoryCTN2() {
        Context _context = getContext();
        new AsyncTask<Void, Void, String>() {
            @Override
            protected String doInBackground(Void... voids) {
                res[0] = model.inventoryCTN2(txtSN2.getText().toString(), txtSN.getText().toString());
                return "OK";
            }

            @Override
            protected void onPreExecute() {
                super.onPreExecute();
            }

            protected void onPostExecute(String result) {
                super.onPostExecute(result);
                if (res[0].isSuccess()) {
                    txtQTY.requestFocus();
                    voiceManager.playOK();
                    MyToast.show(_context, res[0].getMessage(), UtilsConstants.Status.SUCCESS.name());
                } else {
                    txtSN2.setText("");
                    txtSN2.requestFocus();
                    voiceManager.playNG();
                    MyToast.show(_context, res[0].getMessage(), UtilsConstants.Status.ERROR.name());
                }
            }
        }.execute();
    }
    private  void FINISH(){
        etCarton.setText("");
        //txtLocationNo = "";
        txtSN.setText("");
        txtSN2.setText("");
        txtQTY.setText("");
        etCarton.requestFocus();
    }
    private void checkData(){
        if (rdoQTY.isChecked()) {
            if(palletNo.equals("")){
                checkLayout2.setVisibility(VISIBLE);
                checkLayout3.setVisibility(VISIBLE);
                checkLayout1.setVisibility(VISIBLE);
                btnPallet.setVisibility(VISIBLE);
                btnPallet.setEnabled(true);
                txtSN2.setText("");
                txtQTY.setText("");
                etCarton.requestFocus();
            }else {
                checkLayout2.setVisibility(VISIBLE);
                checkLayout3.setVisibility(VISIBLE);
                checkLayout1.setVisibility(VISIBLE);
                btnPallet.setVisibility(VISIBLE);
                btnPallet.setEnabled(true);
                txtSN.setText(palletNo);
                txtSN2.setText("");
                txtSN2.requestFocus();
            }

        } else {
            if(palletNo.equals("")){
                txtSN.setText("");
                etCarton.requestFocus();
                checkLayout2.setVisibility(INVISIBLE);
                checkLayout3.setVisibility(INVISIBLE);
                checkLayout1.setVisibility(VISIBLE);
                btnPallet.setEnabled(false);
                btnPallet.setVisibility(INVISIBLE);
            }
            else {
                txtSN.setText("");
                txtSN.requestFocus();
                checkLayout2.setVisibility(INVISIBLE);
                checkLayout3.setVisibility(INVISIBLE);
                checkLayout1.setVisibility(VISIBLE);
                btnPallet.setEnabled(false);
                btnPallet.setVisibility(INVISIBLE);
            }

        }
    }
    @OnClick(R.id.btnPallet)
    public void onbtnPallet_Click() {

        InventoryPallet();
    }

    @OnClick(R.id.btnEnd)
    public void onbtnEnd_Click() {
        etCarton.setText("");
        txtQTY.setText("");
        txtSN.setText("");
        txtSN2.setText("");
        etCarton.requestFocus();
        lstSNResult = new ArrayList();
        InventoryAdapterLocationSNInfo.setListSNDone(lstSNResult);
        lvList1.setAdapter(null);
        lvList.setAdapter(null);
        lvList2.setAdapter(null);
       // InventoryCTN();
    }
}
