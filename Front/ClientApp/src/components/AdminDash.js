import React, { Component } from 'react';
import { NavMenu } from './NavMenu';

export class AdminDash extends Component {
    static displayName = AdminDash.name;

    render() {
        return (
            <>
                <NavMenu />
            </>
            );
    }
}
