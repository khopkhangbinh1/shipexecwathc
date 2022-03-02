package com.ict.ppsedi.view.adapter.check;

import android.view.View;
import android.view.ViewGroup;
import android.view.LayoutInflater;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.ict.ppsedi.entities.InventoryEntity;
import com.ict.ppsedi.view.fragment.edi.check.InventoryFragment;

import java.util.List;

public class InventoryAdapter extends BaseAdapter  {

    private LayoutInflater flater;
    private List<InventoryEntity> list;
    private int listItemLayoutResource;
    private int textViewItemNameId;
    private int textViewItemPercentId;




    // Arguments example:
    //  @listItemLayoutResource: R.layout.spinner_item_layout_resource
    //        (File: layout/spinner_item_layout_resource.xmll)
    //  @textViewItemNameId: R.id.textView_item_name
    //        (A TextVew in file layout/spinner_item_layout_resource.xmlxml)
    //  @textViewItemPercentId: R.id.textView_item_percent
    //        (A TextVew in file layout/spinner_item_layout_resource.xmll)
    public InventoryAdapter(InventoryFragment context, int listItemLayoutResource,
                            int textViewItemNameId,
                            //int textViewItemPercentId,
                            List<InventoryEntity> list) {
        this.listItemLayoutResource = listItemLayoutResource;

        this.textViewItemNameId = textViewItemNameId;
        //  this.textViewItemPercentId = textViewItemPercentId;
        this.list = list;
        this.flater = context.getLayoutInflater();
    }

    @Override
    public int getCount() {
        if(this.list == null)  {
            return 0;
        }
        return this.list.size();
    }

    @Override
    public Object getItem(int position) {
        return this.list.get(position);
    }
    @Override
    public long getItemId(int position) {
        InventoryEntity Inventory = (InventoryEntity) this.getItem(position);
        return position;
    }
    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        InventoryEntity language = (InventoryEntity) getItem(position);
        View rowView = this.flater.inflate(this.listItemLayoutResource, null,true);

        TextView textViewItemName = (TextView) rowView.findViewById(this.textViewItemNameId);
        textViewItemName.setText(language.getName());


        TextView textViewItemPercent = (TextView) rowView.findViewById(textViewItemPercentId);


        return rowView;
    }
}