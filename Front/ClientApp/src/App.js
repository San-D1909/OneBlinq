import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './pages/Home';
import { Plugins } from './pages/Plugins';
import PluginInfo from './pages/PluginInfo'
import { Login } from './pages/Login';
import { Register } from './pages/Register';
import './custom.css'
import AdminDash from './components/Admin/AdminDash';
import UserDashboard from './pages/UserDashboard';

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route exact path='/plugins' component={Plugins} />
                <Route path="/plugins/:pluginId" component={PluginInfo} />
                <Route exact path='/login' component={Login} />
                <Route exact path='/register' component={Register} />
                <Route exact path='/admin/dashboard' component={AdminDash} />
                <Route exact path='/user/dashboard' component={UserDashboard} />
            </Layout>
        );
    }
}
