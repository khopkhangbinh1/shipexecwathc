package com.ict.ppsedi.utilities;

import com.ict.ppsedi.entities.UserEntity;

public class UserUtils {
    static UserEntity User;

    public static UserEntity getUser() {
        return User;
    }

    public static void setUser(UserEntity user) {
        User = user;
    }
}
