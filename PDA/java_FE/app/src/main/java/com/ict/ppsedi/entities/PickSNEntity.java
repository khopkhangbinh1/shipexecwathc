package com.ict.ppsedi.entities;

public class PickSNEntity {
    String Palletid;
    String PickPalletNo;
    String ShipmentId;
    String CTNNO;
    String UUID;
    String empNo;

    public String getPickPalletNo() {
        return PickPalletNo;
    }

    public void setPickPalletNo(String pickPalletNo) {
        PickPalletNo = pickPalletNo;
    }

    public String getPalletid() {
        return Palletid;
    }

    public void setPalletid(String palletid) {
        Palletid = palletid;
    }

    public String getShipmentId() {
        return ShipmentId;
    }

    public void setShipmentId(String shipmentId) {
        ShipmentId = shipmentId;
    }

    public String getCTNNO() {
        return CTNNO;
    }

    public void setCTNNO(String CTNNO) {
        this.CTNNO = CTNNO;
    }

    public String getUUID() {
        return UUID;
    }

    public void setUUID(String UUID) {
        this.UUID = UUID;
    }

    public String getEmpNo() {
        return empNo;
    }

    public void setEmpNo(String empNo) {
        this.empNo = empNo;
    }
}
