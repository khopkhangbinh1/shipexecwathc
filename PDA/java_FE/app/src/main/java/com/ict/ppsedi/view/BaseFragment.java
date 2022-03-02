package com.ict.ppsedi.view;

import android.app.ProgressDialog;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import com.ict.ppsedi.R;
import com.ict.ppsedi.utilities.UtilsConstants;
import com.ict.ppsedi.view.customs.MyToast;

import butterknife.ButterKnife;

public abstract class BaseFragment extends Fragment implements BaseFragmentView {

    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup parent, Bundle savedInstanseState) {
        View view = baseFragmentView(inflater, parent, savedInstanseState);
        return view;
    }

    public void replaceFrag(Fragment fragment) {
        FragmentManager fragmentManager = getActivity().getSupportFragmentManager();
        FragmentTransaction transaction = fragmentManager.beginTransaction();
        transaction.setCustomAnimations(R.anim.slide_in_right, R.anim.slide_out_left, R.anim.slide_in_left, R.anim.slide_out_right);
        transaction.replace(R.id.flContent, fragment).addToBackStack(fragment.getClass().toString()).commit();
    }

}

interface BaseFragmentView {
    View baseFragmentView(LayoutInflater inflater, ViewGroup parent, Bundle savedInstanceState);
}
