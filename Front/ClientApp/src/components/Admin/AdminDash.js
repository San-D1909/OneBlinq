import * as React from "react";
import { Admin, Resource, ListGuesser, EditGuesser } from 'react-admin';
import { UserFilters, UserList, UserShow, UserEdit, UserCreate } from './Users'
import { ThemeOptions } from '@material-ui/core';
import fakeDataProvider from 'ra-data-fakerest';
import { Redirect } from 'react-router-dom';
import { LogoutButton } from './LogoutButton';

export const newOptions = {

    // theme customizable at https://bareynol.github.io/mui-theme-creator

    palette: {
        type: 'dark',
        primary: {
            main: '#6e44ff',
            contrastText: '#deeff4',
            light: '#efa9ae',
            dark: '#efa9ae',
        },
        secondary: {
            main: '#efa9ae',
        },
        background: {
            default: '#02021e',
            paper: '#1b1d33',
        },
        text: {
            primary: '#edeffc',
            secondary: '#d6d8e5',
            disabled: '#c1c3d0',
            hint: '#9ea0ac',
        },
        info: {
            main: '#efa9ae',
        },
    },
};

const dataProvider = fakeDataProvider({
    licenses: [
        { id: 0, userid: 1, licensename: 'Forms', licensetype: 'Single', isactive: 1, timesactivated: 3, creationtime: '1-1 - 2020', expirationdate: '1-1-2022' },
        { id: 1, userid: 2, licensename: 'Line Height', licensetype: 'Single', isactive: 1, timesactivated: 3, creationtime: '1-1 - 2020', expirationdate: '1-1-2022' },
        { id: 2, userid: 3, licensename: 'Line Height', licensetype: 'Single', isactive: 1, timesactivated: 3, creationtime: '1-1 - 2020', expirationdate: '1-1-2022' },
        { id: 3, userid: 3, licensename: 'Line Height', licensetype: 'Single', isactive: 1, timesactivated: 3, creationtime: '1-1 - 2020', expirationdate: '1-1-2022' },
        { id: 4, userid: 4, licensename: 'Line Height & Forms', licensetype: 'Bundle', isactive: 1, timesactivated: 3, creationtime: '1-1 - 2020', expirationdate: '1-1-2022' },
        { id: 5, userid: 5, licensename: 'Line Height & Forms', licensetype: 'Bundle', isactive: 1, timesactivated: 3, creationtime: '1-1 - 2020', expirationdate: '1-1-2022' },
    ],
    users: [
        { id: 0, userid: 1, licensename: 'Forms', licensetype: 'Single', isactive: 1, timesactivated: 3, creationtime: '1-1 - 2020', expirationdate: '1-1-2022' },
        { id: 1, userid: 2, licensename: 'Line Height', licensetype: 'Single', isactive: 1, timesactivated: 3, creationtime: '1-1 - 2020', expirationdate: '1-1-2022' },
        { id: 2, userid: 3, licensename: 'Line Height', licensetype: 'Single', isactive: 1, timesactivated: 3, creationtime: '1-1 - 2020', expirationdate: '1-1-2022' },
        { id: 3, userid: 3, licensename: 'Line Height', licensetype: 'Single', isactive: 1, timesactivated: 3, creationtime: '1-1 - 2020', expirationdate: '1-1-2022' },
        { id: 4, userid: 4, licensename: 'Line Height & Forms', licensetype: 'Bundle', isactive: 1, timesactivated: 3, creationtime: '1-1 - 2020', expirationdate: '1-1-2022' },
        { id: 5, userid: 5, licensename: 'Line Height & Forms', licensetype: 'Bundle', isactive: 1, timesactivated: 3, creationtime: '1-1 - 2020', expirationdate: '1-1-2022' },
    ],
})

const AdminDash = () => {
    if (localStorage.getItem("token") === null) {
        return (
            <Redirect to="/" />
        )
    }

    if (localStorage.getItem("loggedin")) {
        if (localStorage.getItem("isAdmin") === 'False') {
            return (
                <Redirect to="/user/dashboard/" />
            )
        }
    }
    return (
        <Admin theme={newOptions} dataProvider={dataProvider}>
            <Resource name="licenses"
                list={ListGuesser}
                edit={EditGuesser}
            />
            <Resource name="users"
                list={UserList}
                show={UserShow}
            />
        </Admin>
    );
};

export default AdminDash;