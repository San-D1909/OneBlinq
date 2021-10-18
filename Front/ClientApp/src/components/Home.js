import React, { Component } from 'react';
import { NavMenu } from './NavMenu';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
        <>
            <NavMenu />
        </>
       );
    }
}
