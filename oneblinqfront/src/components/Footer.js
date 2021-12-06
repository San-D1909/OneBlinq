import React, { Component } from 'react';
import './Footer.css'

export class Footer extends Component {
    static displayName = Footer.name;

    render() {
        return (
            <footer className="container">
                <h1 className="pt-5 pb-5">
                    Drop us a line.<br />
                    We know you want to.
                </h1>
                <div className="row">
                    <div className="col">
                        <h5>We're at</h5>
                        <ul>
                            <li>Klokgebouw 175</li>
                            <li>5617 AB</li>
                            <li>Eindhoven (Strijp-S)</li>
                            <li>Netherlands</li>
                        </ul>
                    </div>
                    <div className="col">
                        <h5>Reach us via</h5>
                        <ul>
                            <li>
                                <a href="https://www.facebook.com/stuurmen" target="_blank" rel="noreferrer">Facebook</a>
                            </li>
                            <li>
                                <a href="https://twitter.com/@stuurmen" target="_blank" rel="noreferrer">Twitter</a>
                            </li>
                            <li>
                                <a href="https://www.instagram.com/stuurmen/" target="_blank" rel="noreferrer">Instagram</a>
                            </li>
                            <li>&nbsp;</li>
                            <li>
                                <a href="tel:+31404002856">+31 (0)40 400 28 56</a>
                            </li>
                            <li>
                                <a href="mailto:ahoy@stuur.men">ahoy@stuur.men</a>
                            </li>
                        </ul>
                    </div>
                    <div className="col">
                        <h5>Partners</h5>
                        <img src="https://via.placeholder.com/100x45.png" alt="placeholder partner banner" />
                    </div>
                </div>
            </footer>
        );
    }
}
