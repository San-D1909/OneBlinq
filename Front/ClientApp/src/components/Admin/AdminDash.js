import * as React from "react";
import { Admin, Resource, ListGuesser,EditGuesser} from 'react-admin';
import jsonServerProvider from 'ra-data-json-server';
import { createMuiTheme } from '@material-ui/core/styles';

const dataProvider = jsonServerProvider('http://jsonplaceholder.typicode.com');

const theme = createMuiTheme({
    palette: {
        type: 'dark', // Switching the dark mode on is a single property value change.
    },
});

const AdminDash = () => (

    <Admin theme={theme} dataProvider={dataProvider}>
        <Resource name="users"
            list={ListGuesser}
            edit={EditGuesser}
        />
    </Admin>
);

export default AdminDash;