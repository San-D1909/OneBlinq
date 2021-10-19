import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Plugins } from './components/Plugins';
import { Login } from './components/Login';
import { Register } from './components/Register';
import { AdminDash } from './components/AdminDash';
import lb4provider from 'react-admin-lb4';

import './custom.css'

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route exact path='/plugins' component={Plugins} />
                <Route exact path='/login' component={Login} />
                <Route exact path='/register' component={Register} />
                <Route exact path='/AdminDash' component={AdminDash} />
            </Layout>
        );
    }
}
