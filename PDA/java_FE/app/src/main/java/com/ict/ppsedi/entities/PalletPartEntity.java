package com.ict.ppsedi.entities;

public class PalletPartEntity {
    String ICTPN;
    int QTY;
    int CTNQty;
    String PICKCTN;
    String RESTCTN;

    public String getICTPN() {
        return ICTPN;
    }

    public void setICTPN(String ICTPN) {
        this.ICTPN = ICTPN;
    }

    public int getQTY() {
        return QTY;
    }

    public void setQTY(int QTY) {
        this.QTY = QTY;
    }

    public int getCTNQty() {
        return CTNQty;
    }

    public void setCTNQty(int CTNQty) {
        this.CTNQty = CTNQty;
    }

    public String getPICKCTN() {
        return PICKCTN;
    }

    public void setPICKCTN(String PICKCTN) {
        this.PICKCTN = PICKCTN;
    }

    public String getRESTCTN() {
        return RESTCTN;
    }

    public void setRESTCTN(String RESTCTN) {
        this.RESTCTN = RESTCTN;
    }
}
