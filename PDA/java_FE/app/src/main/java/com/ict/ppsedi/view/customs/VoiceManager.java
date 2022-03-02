package com.ict.ppsedi.view.customs;

import android.content.Context;
import android.content.res.AssetFileDescriptor;
import android.media.MediaPlayer;
import android.widget.Toast;

public class VoiceManager {
    private MediaPlayer ok;
    private MediaPlayer ng;
    final private MediaPlayer mediaPlayer = new MediaPlayer();
    private Context _Context;
    AssetFileDescriptor okDescriptor;
    AssetFileDescriptor ngDescriptor;

    public VoiceManager(Context c) {
        this._Context = c;
//        this.ok = new MediaPlayer();
//        this.ng = new MediaPlayer();
//        try {
//            okDescriptor = c.getAssets().openFd("ok.mp3");
//            ok.setDataSource(okDescriptor.getFileDescriptor(), okDescriptor.getStartOffset(), okDescriptor.getLength());
//            okDescriptor.close();
//            ok.prepare();
//
//            ngDescriptor = c.getAssets().openFd("ng.mp3");
//            ng.setDataSource(ngDescriptor.getFileDescriptor(), ngDescriptor.getStartOffset(), ngDescriptor.getLength());
//            ngDescriptor.close();
//            ng.prepare();
//
//
//        } catch (Exception e) {
//            e.printStackTrace();
//        }
    }

    public void playNG() {
        if (mediaPlayer.isPlaying()) {
            mediaPlayer.stop();
        }
        try {
            mediaPlayer.reset();
            AssetFileDescriptor afd = this._Context.getAssets().openFd("ng.mp3");
            mediaPlayer.setDataSource(afd.getFileDescriptor(), afd.getStartOffset(), afd.getLength());
            mediaPlayer.prepare();
            mediaPlayer.start();
        } catch (IllegalStateException e) {
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public void playOK() {
        if (mediaPlayer.isPlaying()) {
            mediaPlayer.stop();
        }
        try {
            mediaPlayer.reset();
            AssetFileDescriptor afd = this._Context.getAssets().openFd("ok.mp3");
            mediaPlayer.setDataSource(afd.getFileDescriptor(), afd.getStartOffset(), afd.getLength());
            mediaPlayer.prepare();
            mediaPlayer.start();
        } catch (IllegalStateException e) {
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

//    public void playOK() {
//        try {
//            mediaPlayer.reset();
//            ok.start();
//        } catch (Exception e) {
//            e.printStackTrace();
//        }
//    }

//    public void playNG() {
//        try {
//            ng.start();
//        } catch (Exception e) {
//            int i = 1;
//            e.printStackTrace();
//        }
//    }
}
