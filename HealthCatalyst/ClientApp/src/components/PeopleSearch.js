import React, { Component } from 'react';

export class PeopleSearch extends Component {
    displayName = PeopleSearch.name

    constructor(props) {
        super(props);
        this.state = { people: [], loading: true };

        fetch('api/People/People')
            .then(response => response.json())
            .then(data => {
                console.log(data)
                this.setState({ people: data, loading: false });
            });
    }

    static renderPeopleTable(people) {
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
                    {people.map(person =>
                        <tr key={person.firstName}>
                            <td><img id="ItemPreview" alt="No Image" src={'data:image/png;base64,' + person.photo}/></td>
                            <td>{person.firstName}</td>
                            <td>{person.lastName}</td>
                            <td>{person.age}</td>
                            <td>{person.address}</td>
                            <td>{person.interests}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : PeopleSearch.renderPeopleTable(this.state.people);

        return (
            <div>
                {contents}
            </div>
        );
    }
}
