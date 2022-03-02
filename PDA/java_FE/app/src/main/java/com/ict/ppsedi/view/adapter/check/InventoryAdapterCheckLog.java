package com.ict.ppsedi.view.adapter.check;
import android.content.Context;
import android.graphics.Color;
import android.view.View;
import android.view.ViewGroup;
import android.view.LayoutInflater;
import android.widget.BaseAdapter;
import android.widget.LinearLayout;
import android.widget.TextView;

import androidx.recyclerview.widget.RecyclerView;

import com.ict.ppsedi.R;
import com.ict.ppsedi.entities.InventoryEntity;
import com.ict.ppsedi.view.fragment.edi.check.InventoryFragment;

import java.util.ArrayList;
import java.util.List;

public class InventoryAdapterCheckLog extends RecyclerView.Adapter<InventoryAdapterCheckLog.MyViewHolder> {
    private Context mContext;
    private ArrayList<InventoryEntity> listItem;
    private LayoutInflater mLayoutInflater;

    public interface ItemClickListener {
        void onClick(View view, int position, boolean isLongClick);
    }


    public InventoryAdapterCheckLog(Context context, ArrayList<InventoryEntity> array1) {
        // TODO Auto-generated constructor stub
        this.listItem = array1;
        mLayoutInflater = LayoutInflater.from(context);
        this.mContext = context;
    }


    protected class MyViewHolder extends RecyclerView.ViewHolder implements com.ict.ppsedi.view.adapter.check.InventoryAdapterCheckLog.ItemClickListener {

        public TextView  tvCheck, tvPallet, tvpass, tvEror,tvResult;
        LinearLayout llBodyInventoryCheckLog;
        InventoryAdapterCheckLog.ItemClickListener itemClickListener;

        public MyViewHolder(View v) {
            super(v);
            llBodyInventoryCheckLog = v.findViewById(R.id.llBodyInventoryCheckLog);
            tvPallet = v.findViewById(R.id.tvPallet);
            tvCheck = v.findViewById(R.id.tvCheck);
            tvpass = v.findViewById(R.id.tvpass);
            tvEror = v.findViewById(R.id.tvEror);
            tvResult = v.findViewById(R.id.tvResult);
        }

        public void setItemClickListener(InventoryAdapterCheckLog.ItemClickListener itemClickListener) {
            this.itemClickListener = itemClickListener;
        }


        @Override
        public void onClick(View view, int position, boolean isLongClick) {
            itemClickListener.onClick(view, getAdapterPosition(), false);
        }
    }

    @Override
    public InventoryAdapterCheckLog.MyViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View item = mLayoutInflater.inflate(R.layout.inventory_location_log_item, parent, false);
        return new InventoryAdapterCheckLog.MyViewHolder(item);
    }

    @Override
    public void onBindViewHolder(InventoryAdapterCheckLog.MyViewHolder holder, int position) {
        InventoryEntity obj = listItem.get(position);

        holder.tvCheck.setText(obj.getCheckTime());
        holder.tvPallet.setText(obj.getPalletNo());
        holder.tvpass.setText(obj.getPasscartonqty());
        holder.tvEror.setText(obj.getErrorcartonqty());
        holder.tvResult.setText( obj.getResult());




        if (position % 2 == 0) {
            holder.llBodyInventoryCheckLog.setBackgroundColor(Color.parseColor("#FFFFFF"));
        } else
            holder.llBodyInventoryCheckLog.setBackgroundColor(Color.parseColor("#EFEFEF"));
    }

    @Override
    public int getItemCount() {
        return listItem.size();
    }
}


