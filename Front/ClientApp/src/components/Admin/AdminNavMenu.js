import React, { Component } from 'react';
import { Nav, NavItem, NavLink } from 'reactstrap';

class AdminNavMenu extends Component {
    render() {
        return (
            <div>
                <Nav vertical>
                    <NavItem>
                        <NavLink href="/user/dashboard/#/devices/">Devices</NavLink>
                    </NavItem>
                    <NavItem>
                        <NavLink href="/user/dashboard/#/licenses/">Licenses</NavLink>
                    </NavItem>
                </Nav>
            </div>
        );
    }
}

export default AdminNavMenu;