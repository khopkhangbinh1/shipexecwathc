package com.ict.ppsedi.presenter.shipping.pick;

import android.os.AsyncTask;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.ict.ppsedi.entities.ExecuteResult;
import com.ict.ppsedi.entities.PalletInfoEntity;
import com.ict.ppsedi.entities.PalletPartEntity;
import com.ict.ppsedi.entities.PickPartLocationEntity;
import com.ict.ppsedi.entities.PickSNEntity;
import com.ict.ppsedi.model.PickModel;
import com.ict.ppsedi.presenter.Presenter;
import com.ict.ppsedi.utilities.UtilsConstants;
import com.ict.ppsedi.view.activity.shipping.pick.PickDetailView;

import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.List;

public class PickDetailPresenter implements Presenter {
    PickDetailView view;
    PickModel model;
    PalletInfoEntity objDetail;

    Gson gson;
    boolean firstCarton;

    public PickDetailPresenter() {
        model = new PickModel();
        objDetail = new PalletInfoEntity();
        gson = new Gson();
        firstCarton = true;
    }

    @Override
    public void resume() {

    }

    @Override
    public void pause() {

    }

    @Override
    public void destroy() {

    }

    public void setView(PickDetailView _view) {
        this.view = _view;
    }

    public void bindPalletDetail(boolean isBackground) {
        String palletNo = this.view.getPalletNo();
        final ExecuteResult[] res = {new ExecuteResult()};
        new AsyncTask<Void, Void, String>() {
            @Override
            protected String doInBackground(Void... voids) {
                res[0] = model.getPalletInfoDetail(palletNo);
                if (res[0].isSuccess()) {
                    objDetail = gson.fromJson(res[0].getData().toString(), new TypeToken<PalletInfoEntity>() {
                    }.getType());
                }
                return "OK";
            }

            @Override
            protected void onPreExecute() {
                super.onPreExecute();
                if (!isBackground)
                    view.showDialog();
            }

            protected void onPostExecute(String result) {
                super.onPostExecute(result);
                if (res[0].isSuccess()) {
                    view.bindDetail(objDetail);
                } else {
                    view.showMsg(res[0].getMessage(), UtilsConstants.Status.ERROR.name());
                }
                if (!isBackground)
                    view.closeDialog();
            }
        }.execute();
    }

    public void bindPalletPart() {
        String palletNo = this.view.getPalletNo();
        final ExecuteResult[] res = {new ExecuteResult()};
        new AsyncTask<Void, Void, String>() {
            @Override
            protected String doInBackground(Void... voids) {
                res[0] = model.getPalletPart(palletNo);
                return "OK";
            }

            @Override
            protected void onPreExecute() {
                super.onPreExecute();
            }

            protected void onPostExecute(String result) {
                super.onPostExecute(result);
                if (res[0].isSuccess()) {
                    if (res[0].isSuccess()) {
                        ArrayList<PalletPartEntity> lstPart = gson.fromJson(res[0].getData().toString(), new TypeToken<ArrayList<PalletPartEntity>>() {
                        }.getType());
                        view.bindPalletPart(lstPart);
                        bindPartLocation(lstPart.get(0).getICTPN());//bind part-location by first part-no
                    }
                } else {
                    view.showMsg(res[0].getMessage(), UtilsConstants.Status.ERROR.name());
                }
            }
        }.execute();
    }

    public void bindPartLocation(String partNo) {
        String palletNo = this.view.getPalletNo();
        final ExecuteResult[] res = {new ExecuteResult()};
        new AsyncTask<Void, Void, String>() {
            @Override
            protected String doInBackground(Void... voids) {
                res[0] = model.getLocationDetail(palletNo, partNo);
                return "OK";
            }

            @Override
            protected void onPreExecute() {
                super.onPreExecute();
            }

            protected void onPostExecute(String result) {
                super.onPostExecute(result);
                if (res[0].isSuccess()) {
                    if (res[0].isSuccess()) {
                        Type type = new TypeToken<List<PickPartLocationEntity>>() {
                        }.getType();
                        ArrayList<PickPartLocationEntity> lstPart = new Gson().fromJson(res[0].getData().toString(), type);
                        view.bindPartLocation(lstPart);
                    }
                } else {
                    view.showMsg(res[0].getMessage(), UtilsConstants.Status.ERROR.name());
                }
            }
        }.execute();
    }

    public void pick(String cartonNo) {
        PickSNEntity objSN1 = this.view.getSN();

        PickSNEntity objSN = new PickSNEntity();
        objSN.setCTNNO(cartonNo);
        objSN.setPalletid(objSN1.getPalletid());
        objSN.setPickPalletNo(objSN1.getPickPalletNo());
        objSN.setShipmentId(objSN1.getShipmentId());
        objSN.setUUID(objSN1.getUUID());
        objSN.setEmpNo(objSN1.getEmpNo());


        final ExecuteResult[] res = {new ExecuteResult()};
        new AsyncTask<Void, Void, String>() {
            @Override
            protected String doInBackground(Void... voids) {
                res[0] = model.pickCTN(objSN);
                return "OK";
            }

            @Override
            protected void onPreExecute() {
                super.onPreExecute();
                view.scanNewCarton();
                if (firstCarton)
                    view.showDialog();
            }

            protected void onPostExecute(String result) {
                super.onPostExecute(result);
                if (firstCarton)
                    view.closeDialog();

                if (res[0].isSuccess()) {
                    if (firstCarton)
                        firstCarton = false;

                    view.playOK();
                    PalletInfoEntity objRes = new Gson().fromJson(res[0].getData().toString(), new TypeToken<PalletInfoEntity>() {
                    }.getType());
                    objSN.setPickPalletNo(objRes.getPickPalletNo());
                    view.setSN(objSN);
                    view.bindPickPalletNo();
                    refreshPick();
                    view.showMsg("OK", UtilsConstants.Status.SUCCESS.name());
                    view.setLastPickCarton(objSN.getCTNNO());
                    if (res[0].getMessage().indexOf("FINISH") >= 0) {
                        view.showMsg(res[0].getMessage(), UtilsConstants.Status.SUCCESS.name());
                        view.generateLabel(objSN.getCTNNO());
                    }
                } else {
                    view.showMsg(res[0].getMessage(), UtilsConstants.Status.ERROR.name());
                    view.playNG();
                }
            }
        }.execute();
    }

    public void refreshPick() {
        bindPalletDetail(true);
        bindPalletPart();
//        bindPartLocation("ALL");
        //bind material
        //bind location
    }

    public void finishPick() {
        String palletNo = this.view.getPalletNo();
        final ExecuteResult[] res = {new ExecuteResult()};
        new AsyncTask<Void, Void, String>() {
            @Override
            protected String doInBackground(Void... voids) {
                res[0] = model.finishPick(palletNo);
                return "OK";
            }

            @Override
            protected void onPreExecute() {
                super.onPreExecute();
                view.showDialog();
            }

            protected void onPostExecute(String result) {
                super.onPostExecute(result);
                if (res[0].isSuccess()) {
                    String lastCarton = view.getLastPickCarton();
                    if (lastCarton == null || lastCarton == "")
                        view.backActivity();
                    else
                        view.generateLabel(lastCarton);
                } else {
                    view.showMsg(res[0].getMessage(), UtilsConstants.Status.ERROR.name());
                }
                view.closeDialog();
            }
        }.execute();
    }
}
