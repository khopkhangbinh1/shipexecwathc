package com.ict.ppsedi.view.adapter.pick;

import android.content.Context;
import android.graphics.Color;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.LinearLayout;
import android.widget.TextView;

import androidx.recyclerview.widget.RecyclerView;

import com.ict.ppsedi.R;
import com.ict.ppsedi.entities.PickLogEntity;
import com.ict.ppsedi.entities.PickPartLocationEntity;

import java.util.ArrayList;

public class PickLogAdapter extends RecyclerView.Adapter<PickLogAdapter.MyViewHolder> {

    private Context mContext;
    private ArrayList<PickLogEntity> listItem;
    private LayoutInflater mLayoutInflater;

    public interface ItemClickListener {
        void onClick(View view, int position, boolean isLongClick);
    }


    public PickLogAdapter(Context context, ArrayList<PickLogEntity> array1) {
        // TODO Auto-generated constructor stub
        this.listItem = array1;
        mLayoutInflater = LayoutInflater.from(context);
        this.mContext = context;
    }


    protected class MyViewHolder extends RecyclerView.ViewHolder implements PickLogAdapter.ItemClickListener {

        public TextView tvNo, tvShipment, tvPallet, tvEmployee, tvPickTime, tvPickQty;
        LinearLayout llBody;
        PickLogAdapter.ItemClickListener itemClickListener;

        public MyViewHolder(View v) {
            super(v);
            llBody = v.findViewById(R.id.llBody);
            tvNo = v.findViewById(R.id.tvNo);
            tvShipment = v.findViewById(R.id.tvShipment);
            tvPallet = v.findViewById(R.id.tvPallet);
            tvEmployee = v.findViewById(R.id.tvEmployee);
            tvPickTime = v.findViewById(R.id.tvPickTime);
            tvPickQty = v.findViewById(R.id.tvPickQty);
        }

        public void setItemClickListener(PickLogAdapter.ItemClickListener itemClickListener) {
            this.itemClickListener = itemClickListener;
        }


        @Override
        public void onClick(View view, int position, boolean isLongClick) {
            itemClickListener.onClick(view, getAdapterPosition(), false);
        }
    }

    @Override
    public PickLogAdapter.MyViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View item = mLayoutInflater.inflate(R.layout.pick_log_item, parent, false);
        return new PickLogAdapter.MyViewHolder(item);
    }

    @Override
    public void onBindViewHolder(PickLogAdapter.MyViewHolder holder, int position) {
        PickLogEntity obj = listItem.get(position);

        holder.tvNo.setText("" + (position + 1));
        holder.tvShipment.setText("" + obj.getShipmentId());
        holder.tvPallet.setText("" + obj.getPalletNo());
        holder.tvEmployee.setText("" + obj.getEmpNo());

        holder.tvPickTime.setText("" + obj.getStartTime().substring(obj.getStartTime().indexOf(" ")) + "\n" +
                obj.getEndTime().substring(obj.getEndTime().indexOf(" ")));

        holder.tvPickQty.setText("" + obj.getDealQty() + "/" + obj.getQty());


        if (position % 2 == 0) {
            holder.llBody.setBackgroundColor(Color.parseColor("#FFFFFF"));
        } else
            holder.llBody.setBackgroundColor(Color.parseColor("#EFEFEF"));
    }

    @Override
    public int getItemCount() {
        return listItem.size();
    }

}