import React, { Component } from 'react';
import './PluginCard.css'

export class PluginCard extends Component {
    static displayName = PluginCard.name;

    render() {
        const { id, title } = this.props.data
        return (
            <div className="card plugin">
                <div className="card-body">
                    <img src="https://via.placeholder.com/344x216.png" alt={title} />
                </div>
                <div className="card-footer">
                    <a href={id}>{title}</a>
                    <span className="float-end">
                        <span><i className="far fa-sm fa-comment"></i> 123</span>
                        <span className="pl-2"><i className="fas fa-sm fa-arrow-down"></i> 1.7k</span>
                    </span>
                </div>
            </div>
        );
    }
}
