import React, { Component } from 'react';
import { Button, FormControl, Form } from 'react-bootstrap';
import './search.css';

export class Search extends Component {
    displayName = Search.name

    constructor(props) {
        super(props);
        this.state = { people: [], loading: false, query: '', errorMessage: ''};
    }

    searchByName = e => {

        if (this.validateState() === 'error')
            return;

        // Prevent button click from submitting form
        e.preventDefault();
        
        this.setState({ loading: true });

        const searchInput = this.state.query;
        const query = 'api/People/' + searchInput;

        fetch(query)
            .then(response => response.json())
            .then(data => {
                this.setState({ people: data, loading: false });
            });
    }

    renderPeopleTable() {
        if (this.state.loading) {
            return (
                <section className='loader-section'>
                    <div className='flex-item'>
                        <div className='loader'></div>
                    </div>              
                </section>
            )
        }
        else if (this.state.people.length !== 0) {
            return (
                <table className='table'>
                    <thead>
                        <tr>
                            <th></th>
                            <th>First name</th>
                            <th>Last name</th>
                            <th>Age</th>
                            <th>Address</th>
                            <th>Interests</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.people.map(person =>
                            <tr key={person.firstName}>
                                <td><img id="ItemPreview" alt="Profile" src={'.' + person.photo} /></td>
                                <td>{person.firstName}</td>
                                <td>{person.lastName}</td>
                                <td>{person.age}</td>
                                <td>{person.address}</td>
                                <td>{person.interests}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            )
        }
        else {
            return ( <span></span> )
        }
    }

    searchIsValid() {
        if (this.state.query !== '') {
            if (!this.state.query.match(/^[a-zA-Z]+$/))
                return false;
        }

        return true;
    }

    validateState() {
        if (this.searchIsValid()) {
            this.setState({ errorMessage: '' });
            return 'success';
        }

        this.setState({ errorMessage: 'No special characters in search' });

        return 'error';
    }

    handleChange = e => {
        this.setState({ query: e.target.value }, this.validateState);
    }

    submitHandler = e => {
        e.preventDefault();
    }

    renderSearchArea() {
        return (
            <div className='container'>
                <section>
                    <div className='col-md-4'></div>
                    <div className='col-md-4'>
                        <Form onSubmit={this.submitHandler} inline>
                            <FormControl
                                type="text"
                                value={this.state.query}
                                placeholder="Search for person"
                                onChange={this.handleChange}
                            />                     
                            <FormControl.Feedback />                   
                            <Button bsStyle="primary" className="button is-info" onClick={this.searchByName} disabled={!!this.state.errorMessage}>
                                Search
                            </Button>
                        </Form>
                        <div className={this.state.errorMessage ? "error text-danger" : ""}> {this.state.errorMessage}</div>
                    </div>
                    <div className='col-md-4'></div>
                </section>
            </div>
        );
    }

    render() {
        return (
            <div>
                {this.renderSearchArea()}
                <hr />
                {this.renderPeopleTable()}
            </div>
        );
    }
}
