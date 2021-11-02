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

function BundleCard(props) {
    const { bundle } = props
    return (
         <div className = "card plugin" >
            <div className="card-body">
                <img src="https://via.placeholder.com/344x216.png" alt={bundle.title} />
            </div>
            <div className="card-footer">
                <a href={bundle.id}>{bundle.title}</a>
                <span className="float-end">
                    <span><i className="far fa-sm fa-comment"></i> 123</span>
                    <span className="pl-2"><i className="fas fa-sm fa-arrow-down"></i> 1.7k</span>
                </span>
            </div>
        </div >
     )
}

function PluginBundleList(props) {
    const { bundles } = props
    const listItems = bundles.map((bundle, i) =>
        <div key={i} className="col-12 col-lg-4 col-md-6 py-3">
            <BundleCard bundle={bundle} />
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
            plugins: [],
            bundles: []
        }
        this.addPlugin = this.addPlugin.bind(this)
        this.fetchPlugins = this.fetchPlugins.bind(this)
        this.fetchBundles = this.fetchBundles.bind(this)
    }

    componentDidMount() {
        this.setState({ plugins: this.fetchPlugins(), bundles: this.fetchBundles() })
    }

    fetchBundles() {
        // TODO: fetch bundles
        console.log("fetch bundles")
        return [{
            id: 1,
            title: "figma-bundle",
            plugins: [{
                id: 1,
                title: "forms",
                price: 5.00
            }]
        }]
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
                    <PluginBundleList bundles={this.state.bundles} />
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
