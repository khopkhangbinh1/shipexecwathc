package com.ict.ppsedi.view.activity.initialization.login;

import java.util.List;

public interface LoginView {
    void onBtnLoginClick();

    String getUserName();

    String getPassword();

    void handleLogin(String msg, boolean status);

    void showDialog();

    void closeDialog();

    String getVersion();

    void disableBtnLogin();

    void enableBtnLogin();

    void setValidPDAVersion(boolean isValid);

    void showMsg(String msg, String type);
}
