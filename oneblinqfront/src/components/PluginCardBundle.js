import React, { Component } from 'react';
import './PluginCard.css'

export class PluginCard extends Component {
    static displayName = PluginCard.name;

    render() {
        return (
            <div className="card plugin">
                <div className="card-body">
                    <img src="https://via.placeholder.com/344x216.png" />
                </div>
                <div className="card-footer">
                    <a>test</a> 
                    {this.props.data}
                </div>
            </div>
        );
    }
}
