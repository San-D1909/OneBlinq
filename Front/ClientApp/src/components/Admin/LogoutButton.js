import * as React from 'react';
import { forwardRef } from 'react';
import { useLogout } from 'react-admin';
import MenuItem from '@material-ui/core/MenuItem';
import ExitIcon from '@material-ui/icons/PowerSettingsNew';

const LogoutButton = forwardRef((props, ref) => {
    const logout = useLogout();
    const handleClick = () => {
        console.log('click')
        logout();
    }

    return (
        <MenuItem
            onClick={console.log("KUT REACT ADMIN")}
            ref={ref}
        >
            <ExitIcon /> Kanker
        </MenuItem>
    );
});

export default LogoutButton;