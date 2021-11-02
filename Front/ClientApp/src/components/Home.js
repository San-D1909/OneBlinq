import React, { Component } from 'react';
import { NavMenu } from './NavMenu';
import "./Home.css";
import { Button } from 'react-bootstrap';

export class Home extends Component {
    static displayName = Home.name;

    backgroundImage = {
        background: 'url("/images/background-image.svg") no-repeat',
        backgroundSize: 'contain',
        height: '100%',
        width: '100%',
        position: 'absolute',
        bottom: 0,
        backgroundPosition: 'bottom',
    }
    buttonFontSize = {
        fontSize: '1.5rem'
    }
  render () {
    return (
        <>
            <div style={this.backgroundImage} className="bg-homepage-image">
                <NavMenu />
                <div>
                    <h1 className="homeheader">Design just got a hell <br/> of a lot <div className="headerpurple">faster</div></h1>
                    <div className="btnholder">
                        <Button style={this.buttonFontSize} className="m-1 px-4 py-3 btn-oneblinq-roze" href="/plugins">Get started for free</Button>
                        <Button style={this.buttonFontSize} className="m-1 px-4 py-3" variant="outline-light" href="">Watch video</Button>
                    {/* <a className="btngetstarted" href="/register">Get started for free</a>
                    <a className="btnwatchvideo" href="/">Watch video</a> */}
                    </div>
                </div>
            </div>

            </>
        );
    }
}
