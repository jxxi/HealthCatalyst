import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { People } from './components/People';
import { Search } from './components/Search';

export default class App extends Component {
  displayName = App.name

  render() {
    return (
        <Layout>
            <Route path='/' component={Search} />
            <Route path='/People' component={People} />
        </Layout>
    );
  }
}
