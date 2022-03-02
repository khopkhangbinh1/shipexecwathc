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
import com.ict.ppsedi.entities.PalletPartEntity;
import com.ict.ppsedi.entities.PickPartLocationEntity;
import com.ict.ppsedi.presenter.shipping.pick.PickDetailPresenter;

import java.util.ArrayList;

public class PickPartLocationAdapter extends RecyclerView.Adapter<PickPartLocationAdapter.MyViewHolder> {

    private Context mContext;
    private ArrayList<PickPartLocationEntity> listItem;
    private LayoutInflater mLayoutInflater;

    public interface ItemClickListener {
        void onClick(View view, int position, boolean isLongClick);
    }


    public PickPartLocationAdapter(Context context, ArrayList<PickPartLocationEntity> array1) {
        // TODO Auto-generated constructor stub
        this.listItem = array1;
        mLayoutInflater = LayoutInflater.from(context);
        this.mContext = context;
    }


    protected class MyViewHolder extends RecyclerView.ViewHolder implements PickPartLocationAdapter.ItemClickListener {

        public TextView tvPart, tvCTNQty, tvLocation, tvLineNo, tvUDT;
        LinearLayout llBody;
        PickSearchAdapter.ItemClickListener itemClickListener;

        public MyViewHolder(View v) {
            super(v);
            llBody = v.findViewById(R.id.llBody);
            tvPart = v.findViewById(R.id.tvPart);
            tvCTNQty = v.findViewById(R.id.tvCTNQty);
            tvLocation = v.findViewById(R.id.tvLocation);
            tvLineNo = v.findViewById(R.id.tvLineNo);
            tvUDT = v.findViewById(R.id.tvUDT);
        }

        public void setItemClickListener(PickSearchAdapter.ItemClickListener itemClickListener) {
            this.itemClickListener = itemClickListener;
        }


        @Override
        public void onClick(View view, int position, boolean isLongClick) {
            itemClickListener.onClick(view, getAdapterPosition(), false);
        }
    }

    @Override
    public PickPartLocationAdapter.MyViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View item = mLayoutInflater.inflate(R.layout.pick_partlocation_item, parent, false);
        return new PickPartLocationAdapter.MyViewHolder(item);
    }

    @Override
    public void onBindViewHolder(PickPartLocationAdapter.MyViewHolder holder, int position) {
        PickPartLocationEntity obj = listItem.get(position);
        holder.tvLocation.setText("" + obj.getLoc());
        holder.tvPart.setText("" + obj.getICTPN());
        holder.tvCTNQty.setText("" + obj.getCTNQty());
        if (obj.getLineNo() != null)
            holder.tvLineNo.setText("" + obj.getLineNo());
        holder.tvUDT.setText("" + obj.getUdt());

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