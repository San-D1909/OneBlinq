import * as React from "react";
import "./AdminDash.css"
import { Admin, Resource, ListGuesser } from 'react-admin';
import jsonServerProvider from 'ra-data-json-server';
import { NavMenu } from '../NavMenu';

const dataProvider = jsonServerProvider('http://jsonplaceholder.typicode.com');

const AdminDash = () => (

    <Admin dataProvider={dataProvider}>
        <Resource name="users"
            list={ListGuesser}
        />
    </Admin>
);

export default AdminDash;