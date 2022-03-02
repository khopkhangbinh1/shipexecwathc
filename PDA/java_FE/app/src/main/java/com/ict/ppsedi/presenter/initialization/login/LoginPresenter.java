package com.ict.ppsedi.presenter.initialization.login;

import android.os.AsyncTask;

import com.ict.ppsedi.entities.ExecuteResult;
import com.ict.ppsedi.entities.UserEntity;
import com.ict.ppsedi.model.LoginModel;
import com.ict.ppsedi.presenter.Presenter;
import com.ict.ppsedi.utilities.UserUtils;
import com.ict.ppsedi.utilities.UtilsConstants;
import com.ict.ppsedi.view.activity.initialization.login.LoginView;

import java.util.concurrent.Callable;

public class LoginPresenter implements Presenter {
    LoginView view;
    LoginModel model;
    UserEntity userEntity;

    public LoginPresenter() {
        model = new LoginModel();
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

    public void setView(LoginView _view) {
        this.view = _view;
    }

    public void login() {
        userEntity = new UserEntity();
        userEntity.setUserID(this.view.getUserName());
        userEntity.setPassword(this.view.getPassword());
        this.view.showDialog();
        this.model.checkLogin(userEntity, (Callable<String>) () -> {
            String msg = userEntity.isStatus() ? "成功" : "工号或密码不正确";
            boolean res = userEntity.isStatus();
            if (res)
                UserUtils.setUser(userEntity);
            view.handleLogin(msg, res);
            this.view.closeDialog();
            return "";
        });
    }

    public void checkValidVersion() {
        String pdaVersion = view.getVersion();
        final ExecuteResult[] res = {new ExecuteResult()};
        new AsyncTask<Void, Void, String>() {
            @Override
            protected String doInBackground(Void... voids) {
                res[0] = model.getPDAVersion();
                return "OK";
            }

            @Override
            protected void onPreExecute() {
                super.onPreExecute();
                view.disableBtnLogin();
            }

            protected void onPostExecute(String result) {
                super.onPostExecute(result);
                if (res[0].isSuccess()) {
                    String currentVersion = res[0].getData().toString();
                    if (currentVersion.equals(pdaVersion)) {
                        view.enableBtnLogin();
                        view.setValidPDAVersion(true);
                    } else
                        view.showMsg("Please update version " + currentVersion, UtilsConstants.Status.ERROR.name());
                } else {
                    view.showMsg(res[0].getMessage(), UtilsConstants.Status.ERROR.name());
                }
            }
        }.execute();
    }
}
