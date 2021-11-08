import * as React from "react";
import { Admin, Resource, ListGuesser, EditGuesser } from 'react-admin';
import jsonServerProvider from 'ra-data-json-server';
import { createMuiTheme } from '@material-ui/core/styles';
import { ThemeOptions } from '@material-ui/core';
import fakeDataProvider from 'ra-data-fakerest';

export const newOptions: ThemeOptions = {

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

/*const dataProvider = jsonServerProvider('http://jsonplaceholder.typicode.com');*/

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
        { id: 1, licensename: 'Forms', licensekey: 'DFGA-FDAF-ASDEF-QWERQ', amountactivated: 3, creationtime: '1-1 - 2020', expirationdate: '1-1-2022' },
        { id: 2, licensename: 'Line Height', licensekey: 'RQWE-QWERQ-ZXCZ-VCXZ', amountactivated: 3, creationtime: '1-1 - 2020', expirationdate: '1-1-2022' },
    ],
})


// This is an option to enable dark mode.
const theme = createMuiTheme({
    palette: {
        type: 'dark', // Switching the dark mode on is a single property value change.
    },
});

const UserDashboard = () => (
    <Admin theme={newOptions} dataProvider={dataProvider}>
        <Resource name="devices"
            list={ListGuesser}
            edit={EditGuesser}
        />
        <Resource name="licenses"
            list={ListGuesser}
            edit={EditGuesser}
        />
    </Admin>
);

export default UserDashboard;