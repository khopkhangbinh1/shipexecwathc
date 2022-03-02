package com.ict.ppsedi.view.activity.shipping.pick;

import com.ict.ppsedi.entities.PalletInfoEntity;
import com.ict.ppsedi.entities.PalletPartEntity;
import com.ict.ppsedi.entities.PickPartLocationEntity;
import com.ict.ppsedi.entities.PickSNEntity;

import java.util.ArrayList;

public interface PickDetailView {
    void showMsg(String msg, String type);

    void showDialog();

    void closeDialog();

    String getPalletNo();

    void bindDetail(PalletInfoEntity objDetail);

    void playOK();

    void playNG();

    PickSNEntity getSN();

    String getLastPickCarton();

    void setLastPickCarton(String carton);

    void setSN(PickSNEntity obj);

    void scanNewCarton();

    void backActivity();

    void generateLabel(String objSN);

    void bindPalletPart(ArrayList<PalletPartEntity> lstPart);

    void bindPartLocation(ArrayList<PickPartLocationEntity> lst);

    void bindPickPalletNo();

    void showToastTest(String msg);

}

