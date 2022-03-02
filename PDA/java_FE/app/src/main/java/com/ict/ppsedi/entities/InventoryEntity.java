package com.ict.ppsedi.entities;

public class InventoryEntity {
    private String id;

    public String getLocationID() {
        return locationID;
    }

    public void setLocationID(String locationID) {
        this.locationID = locationID;
    }

    private String locationID;
    public String ICTPN ;
    private String name;
    public String palletNo ;
    public String Qty ;
    public String QHQty;
    public String QHCaton ;
    public String cartonQTY ;
    public String dateTime ;
    public String  cartonSN;
    public String  result;
    public String  checkTime;

    public String getCheckTime() {
        return checkTime;
    }

    public void setCheckTime(String checkTime) {
        this.checkTime = checkTime;
    }

    public String CTNSN;
    public String locationNo;

    public String getLocationNo() {
        return locationNo;
    }

    public void setLocationNo(String locationNo) {
        this.locationNo = locationNo;
    }

    public String getCTNSN() {
        return CTNSN;
    }

    public void setCTNSN(String CTNSN) {
        this.CTNSN = CTNSN;
    }



    public String  passcartonqty;
    public String  errorcartonqty;

    public String  getCTNQty() {
        return CTNQty;
    }

    public void setCTNQty(String CTNQty) {
        this.CTNQty = CTNQty;
    }

    public String  CTNQty;

    public String getResult() {
        return result;
    }

    public void setResult(String result) {
        this.result = result;
    }
    public String getDateTime() {
        return dateTime;
    }

    public void setDateTime(String dateTime) {
        this.dateTime = dateTime;
    }
    public String getCartonSN() {
        return cartonSN;
    }

    public void setCartonSN(String cartonSN) {
        this.cartonSN = cartonSN;
    }
    public String getPasscartonqty() {
        return passcartonqty;
    }

    public void setPasscartonqty(String passcartonqty) {
        this.passcartonqty = passcartonqty;
    }
    public String getErrorcartonqty() {
        return errorcartonqty;
    }

    public void setErrorcartonqty(String errorcartonqty) {
        this.errorcartonqty = errorcartonqty;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getPalletNo() {
        return palletNo;
    }

    public void setPalletNo(String palletNo) {
        this.palletNo = palletNo;
    }

    public String getCartonQTY() {
        return cartonQTY;
    }

    public void setCartonQTY(String cartonQTY) {
        this.cartonQTY = cartonQTY;
    }

    public String getQty() {
        return Qty;
    }

    public void setQty(String qty) {
        Qty = qty;
    }

    public String getQHCaton() {
        return QHCaton;
    }

    public void setQHCaton(String QHCaton) {
        this.QHCaton = QHCaton;
    }

    public String getQHQty() {
        return QHQty;
    }

    public void setQHQty(String QHQty) {
        this.QHQty = QHQty;
    }

    public String getICTPN() {
        return ICTPN;
    }
    public void setICTPN(String ICTPN) {
        this.ICTPN = ICTPN;
    }
}
