package com.ict.ppsedi.presenter.shipping.pick;

import android.os.AsyncTask;
import android.util.Log;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.ict.ppsedi.entities.ExecuteResult;
import com.ict.ppsedi.entities.PalletInfoEntity;
import com.ict.ppsedi.entities.UserEntity;
import com.ict.ppsedi.model.PickModel;
import com.ict.ppsedi.presenter.Presenter;
import com.ict.ppsedi.utilities.UtilsConstants;
import com.ict.ppsedi.view.activity.initialization.login.LoginView;
import com.ict.ppsedi.view.fragment.edi.pick.PickSearchView;

import java.util.ArrayList;
import java.util.concurrent.Callable;

public class PickSeachPresenter implements Presenter {
    PickSearchView view;
    PickModel model;
    ArrayList<PalletInfoEntity> lstPallet;
    Gson gson;

    public PickSeachPresenter() {
        model = new PickModel();
        lstPallet = new ArrayList<>();
        gson = new Gson();
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

    public void setView(PickSearchView _view) {
        this.view = _view;
    }

    public void bindList() {
        String date = this.view.getShipDate();
        String shipmentID = this.view.getShipmentID();
        new AsyncTask<Void, Void, String>() {
            @Override
            protected String doInBackground(Void... voids) {
                ExecuteResult res = model.GetListPallet(date, shipmentID);
                if (res.isSuccess()) {
                    lstPallet = gson.fromJson(res.getData().toString(), new TypeToken<ArrayList<PalletInfoEntity>>() {
                    }.getType());
                } else {
                    view.showMsg(res.getMessage(), UtilsConstants.Status.ERROR.name());
                }
                return "OK";
            }

            @Override
            protected void onPreExecute() {
                super.onPreExecute();
                view.showDialog();
            }

            protected void onPostExecute(String result) {
                super.onPostExecute(result);
                view.bindList(lstPallet);
                view.closeDialog();
            }
        }.execute();
    }

    public void navigationDetail(PalletInfoEntity obj) {
        String json = gson.toJson(obj);
        final ExecuteResult[] res = new ExecuteResult[1];
        new AsyncTask<Void, Void, String>() {
            @Override
            protected String doInBackground(Void... voids) {
                res[0] = model.getValidPalletPick(obj.getPalletNo(), view.getMachineCode());
                if (res[0].isSuccess()) {
//                    lstPallet = gson.fromJson(res[0].getData().toString(), new TypeToken<ArrayList<PalletInfoEntity>>() {
//                    }.getType());
                }
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
                    boolean chk = true;
                    if (res[0].getMessage().indexOf("WA") >= 0) {
                        chk = false;
                        view.showMachineDialog(res[0].getMessage(), json, obj.getPalletNo());
                    }
                    if (chk)
                        view.bindDetail(json);
                } else
                    view.showMsg(res[0].getMessage(), UtilsConstants.Status.ERROR.name());
                view.closeDialog();
            }
        }.execute();
    }

    public PalletInfoEntity getPalletDetail(String palletNo) {
        return lstPallet.stream().filter(x -> x.getPalletNo().equals(palletNo)).findFirst().orElse(null);
    }

    public void finishPick(String palletNo) {
        final ExecuteResult[] res = {new ExecuteResult()};
        new AsyncTask<Void, Void, String>() {
            @Override
            protected String doInBackground(Void... voids) {
                res[0] = model.finishPick(palletNo);
                return "OK";
            }

            protected void onPostExecute(String result) {
                super.onPostExecute(result);
                if (!res[0].isSuccess()) {
                    view.showMsg(res[0].getMessage(), UtilsConstants.Status.ERROR.name());
                }
            }
        }.execute();
    }
}
