var authorRegisterViewModel;

// use as register author views view model
function Author(id, name, birthdate) {
    var self = this;

    // observable are update elements upon changes, also update on element data changes [two way binding]
    self.Id = ko.observable(id);
    self.Name = ko.observable(name);
    self.Birthdate = ko.observable(birthdate);

    self.addAuthor = function () {
        var dataObject = ko.toJSON(this);

        // remove computed field from JSON data which server is not expecting
        //delete dataObject.FullName;

        $.ajax({
            url: '/api/WebAPIauthor',
            type: 'post',
            data: dataObject,
            contentType: 'application/json',
            success: function (data) {
                authorRegisterViewModel.authorListViewModel.authors.push(new Author(data.Id, data.Name, data.Birthdate));

                self.Id(null);
                self.Name('');
                self.Birthdate('');
            }
        });
    };
}

// use as author list view's view model
function AuthorList() {

    var self = this;

    // observable arrays are update binding elements upon array changes
    self.authors = ko.observableArray([]);

    self.getAuthors = function () {
        self.authors.removeAll();

        // retrieve authors list from server side and push each object to model's students list
        $.getJSON('/api/WebAPIauthor', function (data) {
            $.each(data, function (key, value) {
                self.authors.push(new Author(value.Id, value.Name, value.Birthdate));
            });
        });
    };


    // remove author. current data context object is passed to function automatically.
    self.removeAuthor = function (author) {
        $.ajax({
            url: '/api/WebAPIauthor/' + author.Id(),
            type: 'delete',
            contentType: 'application/json',
            success: function () {
                self.authors.remove(author);
            }
        });
    };
}


// create index view view model which contain two models for partial views
authorRegisterViewModel = { addAuthorViewModel: new Author(), authorListViewModel: new AuthorList() };


// on document ready
$(document).ready(function () {

    // bind view model to referring view
    ko.applyBindings(authorRegisterViewModel);

    // load student data
    authorRegisterViewModel.authorListViewModel.getAuthors();
});