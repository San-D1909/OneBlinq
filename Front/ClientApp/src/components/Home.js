import React, { Component } from 'react';
import { NavMenu } from './NavMenu';
import "./Home.css";
import { Button } from 'react-bootstrap';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
        <>
            <NavMenu />
            <div>
              <h1 className="homeheader">Design just got a hell <br/> of a lot <div className="headerpurple">faster</div></h1>
              <div className="btnholder">
                <Button className="m-1 btn-oneblinq-roze" href="/login">Get started for free</Button>
                <Button className="m-1" variant="outline-light" href="">Watch video</Button>
                {/* <a className="btngetstarted" href="/register">Get started for free</a>
                <a className="btnwatchvideo" href="/">Watch video</a> */}
              </div>
            </div>
        </>
       );
    }
}
