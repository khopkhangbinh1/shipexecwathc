package com.ict.ppsedi.view.fragment;

import android.content.Intent;
import android.content.res.Resources;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.LinearLayout;

import androidx.fragment.app.Fragment;

import com.ict.ppsedi.MainActivity;
import com.ict.ppsedi.R;
import com.ict.ppsedi.utilities.UtilsConstants;
import com.ict.ppsedi.view.BaseFragment;
import com.ict.ppsedi.view.customs.MyToast;
import com.ict.ppsedi.view.customs.VoiceManager;
import com.ict.ppsedi.view.fragment.edi.check.InventoryFragment;
import com.ict.ppsedi.view.fragment.edi.pick.PickLogFragment;
import com.ict.ppsedi.view.fragment.edi.pick.PickReprintFragment;
import com.ict.ppsedi.view.fragment.edi.pick.PickSearchFragment;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;

public class MainMenuFragment extends BaseFragment {


    @BindView(R.id.lnPick)
    public LinearLayout lnPick;

    View view;

    VoiceManager voiceManager;


    @Override
    public View baseFragmentView(LayoutInflater inflater, ViewGroup parent, Bundle savedInstanceState) {
        view = inflater.inflate(R.layout.fragment_main, parent, false);
        ButterKnife.bind(this, view);
        voiceManager = new VoiceManager(this.getContext());
        return view;
    }

    @OnClick(R.id.lnPick)
    public void lnPick_OnClick() {
        voiceManager.playOK();
        this.replaceFrag(new PickSearchFragment());
    }

    @OnClick(R.id.lnPickLog)
    public void lnPickLog_OnClick() {
        voiceManager.playOK();
        this.replaceFrag(new PickLogFragment());
    }

    @OnClick(R.id.lnRePrint)
    public void lnRePrint_OnClick() {
        voiceManager.playOK();
        this.replaceFrag(new PickReprintFragment());
    }
    @OnClick(R.id.lnPickCheck)
    public void lnPickCheck_OnClick() {
        voiceManager.playOK();
        this.replaceFrag(new InventoryFragment());
    }





//    public void allListener() {
//        lnPick.setOnClickListener(v -> {
//            voiceManager.playOK();
//            this.replaceFrag(new PickSearchFragment());
//        });
//    }
}
