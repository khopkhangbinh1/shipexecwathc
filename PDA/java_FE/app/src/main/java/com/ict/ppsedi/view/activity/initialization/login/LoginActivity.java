package com.ict.ppsedi.view.activity.initialization.login;

import android.app.ProgressDialog;
import android.content.Intent;
import android.content.pm.PackageInfo;
import android.content.res.AssetFileDescriptor;
import android.media.MediaPlayer;
import android.net.Uri;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.KeyEvent;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

import com.ict.ppsedi.MainActivity;
import com.ict.ppsedi.R;
import com.ict.ppsedi.presenter.initialization.login.LoginPresenter;
import com.ict.ppsedi.utilities.UtilsConstants;
import com.ict.ppsedi.view.BaseActivity;
import com.ict.ppsedi.view.customs.MyToast;
import com.ict.ppsedi.view.customs.VoiceManager;

import java.io.File;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;

public class LoginActivity extends BaseActivity implements LoginView {

    @BindView(R.id.etPassword)
    public EditText etPassword;

    @BindView(R.id.etUserName)
    public EditText etUserName;

    @BindView(R.id.tvHost)
    public TextView tvHost;

    @BindView(R.id.tvVersionCode)
    public TextView tvVersionCode;

    @BindView(R.id.btnLogin)
    public Button btnLogin;


    private VoiceManager voiceManager;
    private LoginPresenter presenter;
    private String version;
    private boolean isValidVersion;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        ButterKnife.bind(this);
        voiceManager = new VoiceManager(this);
        presenter = new LoginPresenter();
        presenter.setView(this);
        tvHost.setText(UtilsConstants.WEB_API_ROOT.substring(7));
        tvVersionCode.setText("Version: " + getVersion());
//        etUserName.setText("91314339");
//        etPassword.setText("91314339");
        setEvenListener();
        presenter.checkValidVersion();
    }

    @Override
    public String getVersion() {
        if (this.version == "" || this.version == null)
            try {
                PackageInfo pInfo = this.getPackageManager().getPackageInfo(this.getPackageName(), 0);
                this.version = pInfo.versionName;
            } catch (Exception ex) {
            }
        return this.version;
    }

    @Override
    public void disableBtnLogin() {
        btnLogin.setEnabled(false);
    }

    @Override
    public void enableBtnLogin() {
        btnLogin.setEnabled(true);
    }

    @Override
    public void setValidPDAVersion(boolean isValid) {
        this.isValidVersion = isValid;
    }

    @Override
    public void showMsg(String msg, String type) {
        this.runOnUiThread(() -> MyToast.show(this, msg, type));
    }

    private void setEvenListener() {
        etUserName.setOnKeyListener((v, keyCode, event) -> {
            if (keyCode == UtilsConstants.KEY_SCAN_PDA && event.getAction() == KeyEvent.ACTION_UP
                    && !etUserName.getText().toString().equals("")) {
                voiceManager.playOK();
                etPassword.setText("");
                etPassword.requestFocus();
                return true;
            }
            return false;
        });

        etPassword.setOnKeyListener((v, keyCode, event) -> {
            if (keyCode == UtilsConstants.KEY_SCAN_PDA && etPassword.getText().length() > 0) {
                if (isValidVersion) {
                    presenter.login();
                    return true;
                }
            }
            return false;
        });
    }

    @OnClick(R.id.btnLogin)
    @Override
    public void onBtnLoginClick() {
                etUserName.setText("91314339");
        etPassword.setText("91314339");
        presenter.login();
    }

    @Override
    public String getUserName() {
        return etUserName.getText().toString();
    }

    @Override
    public String getPassword() {
        return etPassword.getText().toString();
    }

    @Override
    public void handleLogin(String msg, boolean status) {
        this.runOnUiThread(() -> {
            if (status) {
                MyToast.show(getBaseContext(), msg, UtilsConstants.Status.SUCCESS.name());
                voiceManager.playOK();
                Intent myIntent = new Intent(this, MainActivity.class);
                startActivity(myIntent);
            } else {
                MyToast.show(getBaseContext(), msg, UtilsConstants.Status.ERROR.name());
                voiceManager.playNG();
            }
        });
    }

    @Override
    public void showDialog() {
        progress.show();
    }

    @Override
    public void closeDialog() {
        progress.dismiss();
    }

    @Override
    public void onResume() {
        super.onResume();
        presenter.checkValidVersion();
        etUserName.setText("");
        etPassword.setText("");
        etUserName.requestFocus();
    }

    @OnClick(R.id.btnDownload)
    public void btnDownload_OnClick() {
        Intent browserIntent = new Intent(Intent.ACTION_VIEW, Uri.parse(UtilsConstants.WEB_API_ROOT + "/app.apk"));
        startActivity(browserIntent);
    }
}
