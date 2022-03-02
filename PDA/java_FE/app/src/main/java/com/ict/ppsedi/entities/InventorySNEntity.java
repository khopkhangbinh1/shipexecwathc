package com.ict.ppsedi.entities;

public class InventorySNEntity {

    String IsFirst;
    String locationNo;
    String locationID;
    String empNo;
    String palletCartonQTY;
    String CTN;
    String CTN2;
    String checkHold;
    String checkCTN;
    String checkCNTPllet;

    public String getCheckCNTPllet() {
        return checkCNTPllet;
    }

    public void setCheckCNTPllet(String checkCNTPllet) {
        this.checkCNTPllet = checkCNTPllet;
    }

    public String getPalletCartonQTY() {
        return palletCartonQTY;
    }
    public void setPalletCartonQTY(String palletCartonQTY) {
        this.palletCartonQTY = palletCartonQTY;
    }
    public String isCheckCTN() {
        return checkCTN;
    }
    public void setCheckCTN(String checkCTN) {
        this.checkCTN = checkCTN;
    }

    public String isCheckHold() {
        return checkHold;
    }
    public void setCheckHold(String checkHold) {
        this.checkHold = checkHold;
    }
    public String getIsFirst() {
        return IsFirst;
    }

    public void setIsFirst(String isFirst) {
        IsFirst = isFirst;
    }

    public String getLocationID() {
        return locationID;
    }

    public void setLocationID(String locationID) {
        this.locationID = locationID;
    }

    public String getCTN2() {
        return CTN2;
    }

    public void setCTN2(String CTN2) {
        this.CTN2 = CTN2;
    }

    public String getLocationNo() {
        return locationNo;
    }

    public void setLocationNo(String locationNo) {
        this.locationNo = locationNo;
    }

    public String getCTNNO() {
        return CTN;
    }

    public void setCTNNO(String CTNNO) {
        this.CTN = CTNNO;
    }

    public String getEmpNo() {
        return empNo;
    }

    public void setEmpNo(String empNo) {
        this.empNo = empNo;
    }
}
