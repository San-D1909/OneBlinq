import React, { Component } from 'react';
import logo from '../wink.gif';

export class Home extends Component {
    static displayName = Home.name;

    render() {
        return (
            <div>
                <img src={logo} alt="loading..." />
            </div>
        );
    }
}
