import React, { Component } from 'react';
import ReactSession from 'react-client-session/dist/ReactSession';
import { Route } from 'react-router';
import AdminDash from './components/Admin/AdminDash';
import { Layout } from './components/Layout';
import './custom.css';
import { ForgotPassword } from './pages/ForgotPassword';
import { Home } from './pages/Home';
import { Login } from './pages/Login';
import { Logout } from './pages/Logout';
import PluginInfo from './pages/PluginInfo';
import { Plugins } from './pages/Plugins';
import { Register } from './pages/Register';
import { ResetPassword } from './pages/ResetPassword';
import UserDashboard from './components/User/UserDashboard';
import { UserInfo } from './pages/UserInfo';
import  VerifyEmail  from './pages/VerifyEmail';

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
                <Route exact path='/resetpassword' component={ResetPassword} />
                <Route exact path='/forgotpassword' component={ForgotPassword} />
                <Route path="/verify/:token" component={VerifyEmail} /> 
                <Route exact path='/UserInfo' component={UserInfo} />
            </Layout>
        );
    }
}
