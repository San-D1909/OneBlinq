import { NavMenu } from '../components/NavMenu';
import React, { useEffect } from 'react';
import "./CSS/VerifyEmail.css";
import axios from 'axios';


export default function VerifyEmail() {

    useEffect(() => {
        console.log("hi")
    });



    return (
        <>
            <NavMenu />
            <h1>You have verified your email</h1>
        </>
    )
}