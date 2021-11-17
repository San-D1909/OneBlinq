import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './pages/Home';
import { Plugins } from './pages/Plugins';
import PluginInfo from './pages/PluginInfo'
import { Login } from './pages/Login';
import { Logout } from './pages/Logout';
import { Register } from './pages/Register';
import { ForgotPassword } from './pages/ForgotPassword';
import './custom.css'
import AdminDash from './components/Admin/AdminDash';
import ReactSession from 'react-client-session/dist/ReactSession';
import UserDashboard from './pages/UserDashboard';

export default class App extends Component {
    static displayName = App.name;

    constructor() {
        super();
        ReactSession.setStoreType("sessionStorage");
    }

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route exact path='/plugins' component={Plugins} />
                <Route path="/plugins/:pluginId" component={PluginInfo} />
                <Route exact path='/login' component={Login} />
                <Route exact path='/logout' component={Logout} />
                <Route exact path='/register' component={Register} />
                <Route exact path='/admin/dashboard' component={AdminDash} />
                <Route exact path='/user/dashboard' component={UserDashboard} />
                <Route exact path='/forgotpassword' component={ForgotPassword} />
            </Layout>
        );
    }
}
