import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { PeopleSearch } from './components/PeopleSearch';

export default class App extends Component {
  displayName = App.name

  render() {
    return (
        <Layout>
            <Route path='/' component={PeopleSearch} />
      </Layout>
    );
  }
}
