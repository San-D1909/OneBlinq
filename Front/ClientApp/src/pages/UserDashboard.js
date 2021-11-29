
import { Admin, Resource, ListGuesser, EditGuesser } from 'react-admin';
import { ThemeOptions } from '@material-ui/core';
import fakeDataProvider from 'ra-data-fakerest';
import AdminNavMenu from "../components/Admin/AdminNavMenu";
import { PluginCreate, PluginEdit, PluginList, PluginShow } from "../components/Admin/Plugins";
import { UserCreate, UserEdit, UserList, UserShow } from "../components/Admin/Users";
import { LicenseList, LicenseShow } from "../components/Admin/License";
import simpleRestProvider from 'ra-data-simple-rest';
import React, { forwardRef } from 'react';
import { useLogout } from 'react-admin';
import MenuItem from '@material-ui/core/MenuItem';
import ExitIcon from '@material-ui/icons/PowerSettingsNew';
import LogoutButton from '../components/Admin/LogoutButton';

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
    devices: [
        { id: 1, devicename: 'LAPTOP-FHVM', licensename: 'Forms', licensekey: 'DFGA-FDAF-ASDEF-QWERQ', activationtime: '1-6-2021' },
        { id: 2, devicename: 'DESKTOP-FHVM', licensename: 'Forms', licensekey: 'DFGA-FDAF-ASDEF-QWERQ', activationtime: '1-6-2021'},
        { id: 3, devicename: 'DESKTOP-XJXJ', licensename: 'Forms', licensekey: 'DFGA-FDAF-ASDEF-QWERQ', activationtime: '8-11-2021'},
        { id: 4, devicename: 'LAPTOP-FHVM', licensename: 'Line Height', licensekey: 'RQWE-QWERQ-ZXCZ-VCXZ', activationtime: '1-6-2021' },
        { id: 5, devicename: 'DESKTOP-FHVM', licensename: 'Line Height', licensekey: 'RQWE-QWERQ-ZXCZ-VCXZ', activationtime: '1-6-2021' },
        { id: 6, devicename: 'DESKTOP-OXIK', licensename: 'Line Height', licensekey: 'RQWE-QWERQ-ZXCZ-VCXZ', activationtime: '8-11-2021' },
    ],
    licenses: [
        { id: 1, plugin: {id: 1, pluginname: 'Testing'}, licensekey: 'DFGA-FDAF-ASDEF-QWERQ', limit: 5, amountactivated: 3, creationtime: '1-1-2020', expirationdate: '1-1-2022' },
        { id: 2, plugin: { id: 1, pluginname: 'Testing' }, licensekey: 'RQWE-QWERQ-ZXCZ-VCXZ', limit: 3, amountactivated: 3, creationtime: '1-1-2020', expirationdate: '1-1-2022' },
    ],
    plugins: [
        { id: 1, pluginname: { en: 'Testing', nl: 'Testen' }, plugindescription: {en: '<p>This is a test</p>', nl: '<p>Dit is een test</p>'}, price: 12.00, pictures: null}
    ]
})

const UserDashboard = () => {

    let protocol = window.location.protocol;
    console.log(protocol);
    return (
        <Admin theme={newOptions} dataProvider={simpleRestProvider(process.env.REACT_APP_API_BACKEND + "/api/v1")} logoutButton={LogoutButton}>
            <Resource name="devices"
                list={ListGuesser}
                edit={EditGuesser}
            />
            <Resource name="licenses"
                list={LicenseList}
                show={LicenseShow}
            />
            <Resource name="plugins" list={PluginList} create={PluginCreate} edit={PluginEdit} show={PluginShow} />
        </Admin>
    );
}

export default UserDashboard;