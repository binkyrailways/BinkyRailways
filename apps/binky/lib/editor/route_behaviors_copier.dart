import 'dart:convert';
import 'package:binky/api.dart';
import 'package:binky/models.dart';

class RouteBehaviorsCopier {
  static Map<String, dynamic> copy(String routeId, List<RouteEvent> events) {
    return {
      "routeId": routeId,
      "events": events.map((e) {
        return {
          "sensorId": e.sensor.id,
          "behaviors": e.behaviors.map((b) {
            return {
              "appliesTo": b.appliesTo,
              "stateBehavior": b.stateBehavior.value,
              "speedBehavior": b.speedBehavior.value,
            };
          }).toList(),
        };
      }).toList(),
    };
  }

  static bool canPaste(String? text, String currentRouteId) {
    if (text == null || text.isEmpty) return false;
    try {
      final decoded = jsonDecode(text);
      if (decoded is! Map) return false;
      if (decoded["routeId"] == currentRouteId) return false;
      final events = decoded["events"];
      if (events is! List) return false;
      if (events.isEmpty) return true;
      final first = events.first;
      return first is Map &&
          first.containsKey("sensorId") &&
          first.containsKey("behaviors");
    } catch (e) {
      return false;
    }
  }

  static Future<void> paste(
      ModelModel model, String routeId, String json) async {
    final Map<String, dynamic> decoded = jsonDecode(json);
    final List<dynamic> eventsData = decoded["events"];

    var currentRoute = await model.getRoute(routeId);

    for (var eventData in eventsData) {
      final sensorId = eventData["sensorId"];
      final behaviorsData = eventData["behaviors"];

      // Find or add event
      var eventIndex =
          currentRoute.events.indexWhere((e) => e.sensor.id == sensorId);
      if (eventIndex < 0) {
        currentRoute = await model.addRouteEvent(routeId, sensorId);
        eventIndex =
            currentRoute.events.indexWhere((e) => e.sensor.id == sensorId);
      }

      // Remove existing behaviors
      final behaviorCount = currentRoute.events[eventIndex].behaviors.length;
      for (var i = behaviorCount - 1; i >= 0; i--) {
        currentRoute = await model.removeRouteEventBehavior(routeId, sensorId, i);
      }

      // Add new behaviors
      for (var bData in behaviorsData) {
        currentRoute = await model.addRouteEventBehavior(routeId, sensorId);
        final behaviorIndex =
            currentRoute.events[eventIndex].behaviors.length - 1;
        final update = currentRoute.clone();
        final behavior = update.events[eventIndex].behaviors[behaviorIndex];
        behavior.appliesTo = bData["appliesTo"];
        behavior.stateBehavior =
            RouteStateBehavior.valueOf(bData["stateBehavior"]) ??
                RouteStateBehavior.RSB_NOCHANGE;
        behavior.speedBehavior =
            LocSpeedBehavior.valueOf(bData["speedBehavior"]) ??
                LocSpeedBehavior.LSB_DEFAULT;
        await model.updateRoute(update);
        currentRoute = await model.getRoute(routeId);
      }
    }
    // Reorder events according to the copied list
    for (var i = 0; i < eventsData.length; i++) {
      final sensorId = eventsData[i]["sensorId"];
      var currentIdx = currentRoute.events.indexWhere((e) => e.sensor.id == sensorId);
      if (currentIdx < 0) continue;
      while (currentIdx > i) {
        currentRoute = await model.moveRouteEventUp(routeId, sensorId);
        currentIdx--;
      }
      while (currentIdx < i) {
        currentRoute = await model.moveRouteEventDown(routeId, sensorId);
        currentIdx++;
      }
    }
  }
}
