package com.ict.ppsedi.entities;

public class PickPartLocationEntity {
    String Area;
    String udt;
    String Loc;
    String ICTPN;
    int CTNQty;
    String LineNo;
    String partcount;
    String total;

    public String getArea() {
        return Area;
    }

    public void setArea(String area) {
        Area = area;
    }

    public String getUdt() {
        return udt;
    }

    public void setUdt(String udt) {
        this.udt = udt;
    }

    public String getLoc() {
        return Loc;
    }

    public void setLoc(String loc) {
        Loc = loc;
    }

    public String getICTPN() {
        return ICTPN;
    }

    public void setICTPN(String ICTPN) {
        this.ICTPN = ICTPN;
    }

    public int getCTNQty() {
        return CTNQty;
    }

    public void setCTNQty(int CTNQty) {
        this.CTNQty = CTNQty;
    }

    public String getLineNo() {
        return LineNo;
    }

    public void setLineNo(String lineNo) {
        LineNo = lineNo;
    }

    public String getPartcount() {
        return partcount;
    }

    public void setPartcount(String partcount) {
        this.partcount = partcount;
    }

    public String getTotal() {
        return total;
    }

    public void setTotal(String total) {
        this.total = total;
    }
}
