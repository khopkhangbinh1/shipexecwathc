package com.ict.ppsedi;

import androidx.annotation.NonNull;
import androidx.appcompat.app.ActionBarDrawerToggle;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;
import androidx.core.view.GravityCompat;
import androidx.drawerlayout.widget.DrawerLayout;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import android.os.Bundle;
import android.view.MenuItem;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.material.navigation.NavigationView;
import com.ict.ppsedi.utilities.UserUtils;
import com.ict.ppsedi.utilities.UtilsConstants;
import com.ict.ppsedi.view.BaseActivity;
import com.ict.ppsedi.view.customs.MyToast;
import com.ict.ppsedi.view.fragment.MainMenuFragment;

public class MainActivity extends BaseActivity implements NavigationView.OnNavigationItemSelectedListener {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);


        Toolbar toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        assert getSupportActionBar() != null;   //null check
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);   //show back button
        getSupportActionBar().setTitle(getString(R.string.app_name));


        DrawerLayout drawer = findViewById(R.id.drawer_layout);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.addDrawerListener(toggle);
        toggle.syncState();

        NavigationView navigationView = findViewById(R.id.nav_view);
        navigationView.setNavigationItemSelectedListener(this);
        beginContext(new MainMenuFragment());

        View header = navigationView.getHeaderView(0);
        TextView userID = header.findViewById(R.id.tvUserID);
        TextView userName = header.findViewById(R.id.tvUserName);
        userID.setText(UserUtils.getUser().getUserID());
        userName.setText(UserUtils.getUser().getUserName());
    }

    @Override
    public boolean onNavigationItemSelected(@NonNull MenuItem item) {
//        int id = item.getItemId();
//        if (id == R.id.nav_wms) {
//            MyToast.show(this, "wms", UtilsConstants.Status.SUCCESS.name());
//        } else if (id == R.id.nav_pick) {
//            MyToast.show(this, "pick", UtilsConstants.Status.SUCCESS.name());
//        } else if (id == R.id.nav_shipment) {
//            MyToast.show(this, "pick", UtilsConstants.Status.SUCCESS.name());
//        } else if (id == R.id.nav_upload856) {
//            MyToast.show(this, "pick", UtilsConstants.Status.SUCCESS.name());
//        }
        switch (item.getItemId()) {
            case R.id.nav_wms:
                MyToast.show(this, "wms", UtilsConstants.Status.SUCCESS.name());
                break;
            case R.id.nav_pick:
                MyToast.show(this, "nav_pick", UtilsConstants.Status.SUCCESS.name());
                break;
            case R.id.nav_shipment:
                MyToast.show(this, "nav_shipment", UtilsConstants.Status.SUCCESS.name());
                break;
            case R.id.nav_upload856:
                MyToast.show(this, "nav_upload856", UtilsConstants.Status.SUCCESS.name());
                break;
        }

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }

    public void beginContext(Fragment fragment) {
        FragmentManager fragmentManager = getSupportFragmentManager();
        FragmentTransaction transaction = fragmentManager.beginTransaction();
        transaction.replace(R.id.flContent, fragment).addToBackStack(fragment.getClass().getName()).commit();
    }

    @Override
    public void onBackPressed() {
        String getCurrentFragment = getCurrentFragment();
        getSupportActionBar().setTitle(R.string.app_name);
        if (getSupportFragmentManager().getBackStackEntryCount() == 1) {
            if (getCurrentFragment.equalsIgnoreCase("MainMenuFragment"))
                finish();
        } else if (getCurrentFragment.equalsIgnoreCase("MainMenuFragment")) {
            finish();
        } else {
            super.onBackPressed();
        }
    }

    public String getCurrentFragment() {
        return this.getSupportFragmentManager().findFragmentById(R.id.flContent).getClass().getSimpleName();
    }

}