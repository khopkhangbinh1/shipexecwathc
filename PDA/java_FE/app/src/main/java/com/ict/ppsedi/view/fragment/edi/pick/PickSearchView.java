package com.ict.ppsedi.view.fragment.edi.pick;

import com.ict.ppsedi.entities.PalletInfoEntity;

import java.util.ArrayList;
import java.util.concurrent.Callable;

public interface PickSearchView {
    void bindList(ArrayList<PalletInfoEntity> lst);

    String getShipDate();

    String getShipmentID();

    void showMsg(String msg, String type);

    void showDialog();

    void closeDialog();

    void bindDetail(String jsonObj);

    String getMachineCode();

    void showMachineDialog(String msg, String json, String palletNo);
//    void showMachineDialog(String msg, Callable call);
}
