import React, { Component } from 'react';
import { NavMenu } from './NavMenu';
import { PluginCard } from './PluginCard'
import { Footer } from './Footer'

function PluginList(props) {
    const { plugins } = props
    const listItems = plugins.map((plugin, i) =>
        <div key={i} className="col-12 col-lg-4 col-md-6 py-3">
            <PluginCard data={plugin} />
        </div>
    )
    return (
        <div className="row">
            {listItems}
        </div>
    )
}

export class Plugins extends Component {
    static displayName = Plugins.name;
    constructor(props) {
        super(props)
        this.state = {
            plugins: []
        }
        this.addPlugin = this.addPlugin.bind(this)
        this.fetchPlugins = this.fetchPlugins.bind(this)
    }

    componentDidMount() {
        this.setState({ plugins: this.fetchPlugins()})
    }

    fetchPlugins() {
        // TODO: fetch plugins
        console.log('fetch')
        return [{
            id: "1",
            title: "forms"
        }, {
            id: "1",
            title: "forms"
        }, {
            id: "1",
            title: "forms"
        }, {
            id: "1",
            title: "forms"
        }, {
            id: "1",
            title: "forms"
        }, {
            id: "1",
            title: "forms"
        }]
    }

    addPlugin() {
        // TODO: remove, meant for testing during development
        const p = {
            id: "1",
            title: "forms"
        }
        this.setState({ plugins: this.state.plugins.concat(p).concat(p).concat(p)})
    }


    render() {
        return (
            <>
                <NavMenu />
                <h1 className="row justify-content-center" style={{ color: '#edeffc' }} >Plugin Bundles</h1>
                <h1 className="row justify-content-center" style={{ color: '#edeffc' }} >Plugins</h1>
                <div className="container">
                    <PluginList plugins={this.state.plugins} />
                    <div className="row justify-content-center">
                        <button onClick={this.addPlugin} className="btn btn-outline-pastel w-25 m-2"><span>show more</span></button>
                    </div>
                </div>
                <Footer />
            </>
        );
    }
}
