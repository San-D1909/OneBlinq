import React, { useState } from 'react';
import { useParams } from "react-router-dom";
import { Modal, Button } from 'react-bootstrap';
import axios from 'axios';

function Buy() {
    console.log("Buying");
}

function fetchLicenseTypes() {
    return axios.get(process.env.REACT_APP_API_BACKEND + '/api/v1/LicenseType').then(response => {
        console.log(response);
        return response;
    })
}

function PluginBuyButton() {
    const params = useParams()
    console.log(params)

    const buttonFontSize = {
        fontSize: '1.5rem'
    };

    const buyButtonStyle = {
        fontSize: '1.5rem',
        backgroundColor: '#efa9ae'
    }

    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => {
        let data = fetchLicenseTypes();
        setShow(true);
    };

    return (
        <>
            <Button style={buyButtonStyle} className="m-1 px-4 py-3" variant="outline-light" onClick={handleShow}>
                Buy
            </Button>

            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Modal heading</Modal.Title>
                </Modal.Header>
                <Modal.Body>Woohoo, you're reading this text in a modal!</Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant="primary" onClick={handleClose}>
                        Save Changes
                    </Button>
                </Modal.Footer>
            </Modal>
        </>
    );
}

export { PluginBuyButton };