import React from 'react';
import Card from 'reactstrap/lib/Card';
import CardBody from 'reactstrap/lib/CardBody';
import CardImg from 'reactstrap/lib/CardImg';
import { NavMenu } from '../components/NavMenu';
import Button from 'reactstrap/lib/Button';
import { Footer } from '../components/Footer';

import { useParams } from "react-router-dom";

export default function PluginInfo() {
    const params = useParams()
    console.log(params)

    const buttonFontSize = {
        fontSize: '1.5rem'
    };

    const textStyling = {
        color: 'white'
    };

    const hrStyling = {
        height: "2px",
        opacity: 0.80,
        color: "#edeffc"
    };

    return (
        <>
            <NavMenu />
            <div className="row p-0 mx-auto logincontainer">

                <h1 className="row m-0 justify-content-center" style={{ color: '#edeffc' }} >Plugin { params.pluginId }</h1>
                <hr style={hrStyling} className="container" />


                    <div className="col-12 col-lg-6 p-1">

                        <Card className="loginformcard h-100 order-last">
                            <CardBody className="p-0">
                                <CardImg className="h-100" src="https://via.placeholder.com/344x216.png" />
                            </CardBody>
                        </Card>
                    </div>
                    <div className="col-12 col-lg-6 p-1">
                        <p style={textStyling}>"Test description."</p>
                    </div>

                    <div className="btnholder">
                        <Button style={buttonFontSize} className="m-1 px-4 py-3 btn-oneblinq-roze" href="/plugins">Buy</Button>
                        <Button style={buttonFontSize} className="m-1 px-4 py-3" variant="outline-light" href="">Add to wishlist</Button>
                    </div>
            </div>
            <Footer />
        </>
    )
}