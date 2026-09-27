using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Carbon.Domain.Contracts.Services.Emissions;

namespace Carbon.Application.Dtos.Emissions;

public record EmissionsRequestDto(
    [Required(ErrorMessage = "Flights field is required!")]
    [MinLength(1, ErrorMessage = "At least one flight must be informed!")]
    List<FlightLegDto> Flights,

    [property: JsonConverter(typeof(JsonStringEnumConverter<CabinClass>))]
    CabinClass Cabin = CabinClass.Economy,

    [Range(1, 100, ErrorMessage = "Passengers must be between 1 and 100!")]
    int Passengers = 1,

    bool RoundTrip = false);

public record FlightLegDto(
    [Required(ErrorMessage = "Origin field is required!")]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "Origin must be a 3-letter IATA code!")]
    string Origin,

    [Required(ErrorMessage = "Destination field is required!")]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "Destination must be a 3-letter IATA code!")]
    string Destination,

    [Required(ErrorMessage = "CarrierCode field is required!")]
    [StringLength(2, MinimumLength = 2, ErrorMessage = "CarrierCode must have 2 characters!")]
    string CarrierCode,

    [Range(1, 9999, ErrorMessage = "FlightNumber must be between 1 and 9999!")]
    int FlightNumber,

    [Required(ErrorMessage = "DepartureDate field is required!")]
    DateOnly DepartureDate);