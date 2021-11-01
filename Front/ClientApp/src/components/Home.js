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

    render() {
        return (
            <>
                <NavMenu />
                <div style={this.backgroundImage} className="bg-homepage-image">
                    <div>
                        <h1 className="homeheader">Design just got a hell <br /> of a lot <div className="headerpurple">faster</div></h1>
                        <div className="btnholder">
                            <Button className="m-1 btn-oneblinq-roze" href="/login">Get started for free</Button>
                            <Button className="m-1" variant="outline-light" href="">Watch video</Button>
                        </div>
                    </div>
                </div>

            </>
        );
    }
}
