import React from 'react';
import Card from 'reactstrap/lib/Card';
import CardBody from 'reactstrap/lib/CardBody';
import CardImg from 'reactstrap/lib/CardImg';
import { NavMenu } from '../components/NavMenu';

export default function PluginInfo() {
    return (
        <>
            <NavMenu />
            
            <h1>test</h1>

            <div className="row p-0 mx-auto logincontainer">
                <div className="col-12 col-lg-6 p-1">
                    <Card className="h-100">
                        <CardBody>
                            <h1 className="text-center"></h1>
                        </CardBody>
                    </Card>
                </div>
                <div className="col-12 col-lg-6 p-1">

                    <Card className="loginformcard h-100 order-last">
                        <CardBody className="p-0">
                            <CardImg className="h-100" src="https://via.placeholder.com/344x216.png" />
                        </CardBody>
                    </Card>
                </div>
            </div>

            
        </>
    )
}