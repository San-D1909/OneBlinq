import * as React from "react";
import { Admin, Resource, ListGuesser, EditGuesser } from 'react-admin';
import { UserFilters, UserList, UserShow, UserEdit, UserCreate } from './Users'
import { Link, Redirect } from 'react-router-dom';
import simpleRestProvider from 'ra-data-simple-rest';
import { LicenseList, LicenseShow } from "./License";
import { PluginList, PluginShow, PluginEdit, PluginCreate } from "./Plugins";
import { LogoutButton } from './LogoutButton';
import { LicenseTypeCreate, LicenseTypeList, LicenseTypeEdit, LicenseTypeShow } from "./LicenseTypes";

export const newOptions = {
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
        <Admin theme={newOptions} dataProvider={simpleRestProvider(process.env.REACT_APP_API_BACKEND + "/api/v1/admin")}>
            <Resource name="license" list={LicenseList} show={LicenseShow} />
            <Resource name="plugin" list={PluginList} create={PluginCreate} edit={PluginEdit} show={PluginShow} />
            <Resource name="user" list={UserList} create={UserCreate} edit={UserEdit} show={UserShow} />
            <Resource name="licenseType" list={LicenseTypeList} create={LicenseTypeCreate} edit={LicenseTypeEdit} show={LicenseTypeShow} />
        </Admin>
    );
};

export default AdminDash;